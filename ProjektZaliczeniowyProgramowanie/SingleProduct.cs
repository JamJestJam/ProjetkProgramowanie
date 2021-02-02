using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DBconnectShop {
    public class SingleProduct {
        public int id { get; }

        public Product Product { get; private set; }

        public SingleProduct(int ID) {
            id = ID;
            Reload();
        }

        public void Reload() {
            using var shop = new Shop();

            var product = shop.Products
                .Include(a => a.Products_Prices)
                .Include(a => a.Product_Images)
                .Include(a => a.Product_Specifications)
                .Include(a => a.Product_Ratings)
                .Include(a => a.Product_Opinions)
                .Where(a => a.Product_id == id);

#if DEBUG
            Console.WriteLine(product.ToQueryString());
#endif

            if(product.Count() != 1)
                throw new IndexOutOfRangeException();

            Product = product.First();
        }
    }
}
