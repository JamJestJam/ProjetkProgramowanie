using DBconnectShop.Access;
using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DBconnectShop.Addons {
    public class Basket {
        Login login;
        public Dictionary<Product, uint> ProductList { get; } = new Dictionary<Product, uint>();

        public long Count =>
            ProductList.Sum(a => a.Value);

        public delegate void BasketContentChange(Basket basket, BasketChangeEventArgs e);

        public event BasketContentChange OnChange;

        public Basket(Login login) {
            this.login = login;

            try {
                int _ = login.GetUserID;
            } catch {
                throw new AuthorizationException();
            }
        }

        public void AddProduct(int productID, uint count = 1) {
            using var db = new Shop();

            var product = db.Products
                .Include(a=>a.Products_Prices)
                .FirstOrDefault(a => a.Product_id == productID);

            if(product is null)
                throw new AddElementException("Produkt o podanym ID nie istnieje");
            uint before = 0;
            uint after = count;

            if(ProductList.ContainsKey(product)) {
                before = ProductList[product];
                ProductList[product] += count;
                after = ProductList[product];
            } else {
                ProductList.Add(product, count);
            }

            OnChange?.Invoke(this, new BasketChangeEventArgs(product, before, after));
        }

        public void SetCount(int productID, uint count) {
            var product = ProductList.FirstOrDefault(a => a.Key.ID == productID).Key;

            if(product is null)
                AddProduct(productID, count);
            else {
                uint before = ProductList[product];
                ProductList[product] = count;
                uint after = ProductList[product];

                OnChange?.Invoke(this, new BasketChangeEventArgs(product, before, after));
            }
        }

        public void Remove(int productID) {
            var product = ProductList.FirstOrDefault(a => a.Key.ID == productID).Key;

            if(product != null) {
                uint before = ProductList[product];
                ProductList.Remove(product);
                uint after = ProductList[product];

                OnChange?.Invoke(this, new BasketChangeEventArgs(product, before, after));
            }
        }
    }
}
