using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop {
    public class BuyableProducts {
        List<Product> Products { get; set; }
        List<Product_categori> Categoris { get; set; }

        public IReadOnlyList<Product_categori> CatergorisRO => Categoris.OrderBy(a=>a.ParentID).ToList().AsReadOnly();
        public IReadOnlyList<Product> ProductsRO => Products.AsReadOnly();

        public BuyableProducts() {
            Refresh();
        }

        public void Refresh() {
            using var db = new Shop();

            var category = db.Product_Categories;

            var products = db.Products
                .Include(a => a.Products_Prices)
                .Include(a => a.Product_Categori)
                .Where(a => a.Product_aviable);

#if DEBUG
            Console.WriteLine(products.ToQueryString());
            Console.WriteLine(category.ToQueryString());
#endif

            Products = products.ToList();
            Categoris = category.ToList();
        }

        public List<Product> GetProducts(int page, int perPage, int? categoryID, string like = "") {
            perPage++;
            page++;
            var categoryWhere = Categoris
                .Where(a => a.Product_category_id == categoryID)
                .SelectManyRecursive(a => a.Children)
                .Select(a => a.Product_category_id).ToList();

            if(categoryID == null)
                categoryWhere = Categoris.SelectManyRecursive(a => a.Children)
                    .Select(a => a.Product_category_id).ToList();
            else
                categoryWhere.Add((int)categoryID);

            return this.Products
                .Where(a => categoryWhere.Any(b => b == a.Product_category_id))
                .Where(a => a.Product_name.Contains(like))
                .ToList();
        }
    }

    internal static class Extension {
        public static IEnumerable<T> SelectManyRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector) {
            var result = source.SelectMany(selector);
            return !result.Any() ? result : result.Union(result.SelectManyRecursive(selector));
        }
    }
}
