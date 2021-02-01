using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop {
    public class BuyableProducts {
        List<Product> Products { get; set; }
        List<Product_categori> Categoris { get; set; }
        List<int> CategorisWhere { get; set; }

        public int ProductCount => Products.Count();


        public BuyableProducts(int? id) {
            Refresh(id);
        }

        public void Refresh(int? id) {
            using var db = new Shop();

            var category = db.Product_Categories;

            var categoryWhere = category.ToList()
                .Where(a => a.Product_category_id == id)
                .SelectManyRecursive(a => a.Children)
                .Select(a => a.Product_category_id).ToList();
            if(id == null)
                categoryWhere = category.SelectManyRecursive(a => a.Children)
                    .Select(a => a.Product_category_id).ToList();
            else
                categoryWhere.Add((int)id);

            var products = db.Products
                .Include(a => a.Products_Prices)
                .Include(a => a.Product_Categori)
                .Where(a => a.Product_aviable)
                .Where(a => categoryWhere.Any(b => b == a.Product_category_id));

#if DEBUG
            Console.WriteLine(products.ToQueryString());
            Console.WriteLine(category.ToQueryString());
#endif

            Products = products.ToList();
            Categoris = category.ToList();
            CategorisWhere = categoryWhere.ToList();
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

        public List<(string name, int id, int? parentID)> ProductCategory() {
            var list = new List<(string name, int id, int? parentID)>();

            foreach(var category in Categoris) {
                var tmp = (
                    name: category.Product_category_name.Trim(),
                    id: category.Product_category_id,
                    parentID: category.Product_sub_category
                );

                list.Add(tmp);
            }

            return list;
        }
    }

    internal static class Extension {
        public static IEnumerable<T> SelectManyRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector) {
            var result = source.SelectMany(selector);
            if(!result.Any()) {
                return result;
            }
            return result.Union(result.SelectManyRecursive(selector));
        }
    }
}
