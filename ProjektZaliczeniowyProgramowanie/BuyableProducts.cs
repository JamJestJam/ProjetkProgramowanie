using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop {
    public class BuyableProducts {
        List<Product> Products { get; set; }

        public int ProductCount => Products.Count();


        public BuyableProducts() {
            Refresh();
        }

        public void Refresh() {
            using var db = new Shop();

            IQueryable<Product> products = db.Products
                .Include(a=>a.Products_Prices)
                .Where(a => a.Product_aviable);
#if DEBUG
            Console.WriteLine(products.ToQueryString());
#endif

            Products = products.ToList();
        }

        public List<(string ProductName, decimal Products_price)> GetProducts(int page = 0, int perPage = 18) {
            var resoult = new List<(string ProductName, decimal Products_price)>();

            for(int i = page * perPage; i < (page + 1) * perPage && i < Products.Count; i++) {
                var product = Products[i];
                var price = product.Products_Prices.OrderBy(a => a.Product_price_date).First();

                var tmp = (
                    ProductName: product.Product_name.Trim(),
                    Products_price: price.Product_price
                );
                resoult.Add(tmp);
            }
            return resoult;
        }
    }
}
