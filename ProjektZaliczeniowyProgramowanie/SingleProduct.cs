using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DBconnectShop {
    public class SingleProduct {
        public int Id { get; }

        public Product Product { get; private set; }

        public SingleProduct(int ID) {
            Id = ID;
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
                    .ThenInclude(b => b.User)
                .Where(a => a.Product_id == Id);

#if DEBUG
            Console.WriteLine(product.ToQueryString());
#endif

            if(product.Count() != 1)
                throw new IndexOutOfRangeException();

            Product = product.First();
        }
    }
}
