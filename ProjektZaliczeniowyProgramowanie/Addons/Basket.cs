using DBconnectShop.Access;
using DBconnectShop.Table;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DBconnectShop.Addons {
    public class Basket {
        Login login;
        Dictionary<Product, uint> productList = new Dictionary<Product, uint>();

        public long Count =>
            productList.Sum(a=>a.Value);

        public IReadOnlyDictionary<Product, uint> Product =>
            new ReadOnlyDictionary<Product, uint>(productList);

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
                .FirstOrDefault(a => a.Product_id == productID);

            if(product is null)
                throw new AddElementException("Produkt o podanym ID nie istnieje");
            uint before = 0;
            uint after = count;

            if(productList.ContainsKey(product)) {
                before = productList[product];
                productList[product] += count;
                after = productList[product];
            } else {
                productList.Add(product, count);
            }

            OnChange?.Invoke(this, new BasketChangeEventArgs(product, before, after));
        }

        public void SetCount(int productID, uint count) {
            var product = productList.FirstOrDefault(a => a.Key.ID == productID).Key;

            if(product is null)
                AddProduct(productID, count);
            else {
                uint before = productList[product];
                productList[product] = count;
                uint after = productList[product];

                OnChange?.Invoke(this, new BasketChangeEventArgs(product, before, after));
            }
        }

        public void Remove(int productID) {
            var product = productList.FirstOrDefault(a => a.Key.ID == productID).Key;

            if(product != null) {
                uint before = productList[product];
                productList.Remove(product);
                uint after = productList[product];

                OnChange?.Invoke(this, new BasketChangeEventArgs(product, before, after));
            }
        }
    }
}
