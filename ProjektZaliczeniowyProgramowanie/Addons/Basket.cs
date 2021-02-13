using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop.Addons {
    public class Basket {
        public int? Address_id { get; set; } = null;
        public Dictionary<int, uint> ProductList { get; } = new Dictionary<int, uint>();

        public long Count =>
            ProductList.Sum(a => a.Value);

        public Basket() { }

        public void AddProduct(int productID, uint count = 1) {
            uint before = 0;
            uint after = count;

            if(ProductList.ContainsKey(productID)) {
                before = ProductList[productID];
                ProductList[productID] += count;
                after = ProductList[productID];
            } else {
                ProductList.Add(productID, count);
            }

            OnChange?.Invoke(this, new BasketChangeEventArgs(productID, before, after));
        }

        public void SetCount(int productID, uint count) {
            if(!ProductList.ContainsKey(productID))
                AddProduct(productID, count);
            else {
                uint before = ProductList[productID];
                ProductList[productID] = count;
                uint after = ProductList[productID];

                OnChange?.Invoke(this, new BasketChangeEventArgs(productID, before, after));
            }
        }

        public void Remove(int productID) {
            if(ProductList.ContainsKey(productID)) {
                uint before = ProductList[productID];
                ProductList.Remove(productID);
                uint after = ProductList[productID];

                OnChange?.Invoke(this, new BasketChangeEventArgs(productID, before, after));
            }
        }

        public delegate void BasketContentChange(Basket basket, BasketChangeEventArgs e);

        public event BasketContentChange OnChange;
    }
}
