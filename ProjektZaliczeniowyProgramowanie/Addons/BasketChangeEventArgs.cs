using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBconnectShop.Table;

namespace DBconnectShop.Addons {
    public class BasketChangeEventArgs : EventArgs {
        uint CountBefore { get; }
        uint CountAfter { get; }
        Product Product { get; }

        internal BasketChangeEventArgs(Product product, uint countBefore, uint countAfter) {
            Product = product;
            CountBefore = countBefore;
            CountAfter = countAfter;
        }
    }
}
