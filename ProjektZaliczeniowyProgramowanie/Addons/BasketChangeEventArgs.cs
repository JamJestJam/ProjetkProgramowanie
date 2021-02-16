using System;

namespace DBconnectShop.Addons {
    /// <summary>
    /// Event zmiany zawartości koszyka
    /// </summary>
    public class BasketChangeEventArgs : EventArgs {
        uint CountBefore { get; }
        uint CountAfter { get; }
        int ProductID { get; }

        internal BasketChangeEventArgs(int product, uint countBefore, uint countAfter) {
            ProductID = product;
            CountBefore = countBefore;
            CountAfter = countAfter;
        }
    }
}
