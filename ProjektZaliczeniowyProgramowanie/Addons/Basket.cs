using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop.Addons {
    /// <summary>
    /// Koszyk
    /// </summary>
    public class Basket {
        /// <summary>
        /// Id addresu przesyłki
        /// </summary>
        public int? Address_id { get; set; } = null;
        /// <summary>
        /// ID produktu i jego ilość
        /// </summary>
        public Dictionary<int, uint> ProductList { get; } = new Dictionary<int, uint>();
        /// <summary>
        /// Ilość produktów w koszyku
        /// </summary>
        public long Count =>
            ProductList.Sum(a => a.Value);
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Basket() { }

        /// <summary>
        /// Dodaje nowy produkt do koszyka
        /// </summary>
        /// <param name="productID">Id produktu</param>
        /// <param name="count">Ilosć produktów</param>
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

        /// <summary>
        /// Ustawia ilośc produktu w koszyku
        /// </summary>
        /// <param name="productID">ID produktu</param>
        /// <param name="count">Ilość</param>
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
        /// <summary>
        /// Usówa produkt
        /// </summary>
        /// <param name="productID">Id produktu</param>
        public void Remove(int productID) {
            if(ProductList.ContainsKey(productID)) {
                uint before = ProductList[productID];
                ProductList.Remove(productID);
                uint after = ProductList[productID];

                OnChange?.Invoke(this, new BasketChangeEventArgs(productID, before, after));
            }
        }

        /// <summary>
        /// Delegat zdarzenia
        /// </summary>
        /// <param name="basket">koszyk w którym zmieniono</param>
        /// <param name="e">event</param>
        public delegate void BasketContentChange(Basket basket, BasketChangeEventArgs e);
        /// <summary>
        /// Zdarzenie wywoływane po zmianie ilośći produktu
        /// </summary>
        public event BasketContentChange OnChange;
    }
}
