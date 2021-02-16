using DBconnectShop.Addons;
using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop.Access {
    /// <summary>
    /// Lista produktów
    /// </summary>
    public class BuyableProducts {
        List<Product> Products { get; set; }
        List<Product_categori> Categoris { get; set; }

        /// <summary>
        /// Lista kategorii
        /// </summary>
        public IReadOnlyList<Product_categori> CatergorisRO => Categoris.OrderBy(a => a.ParentID).ToList().AsReadOnly();
        /// <summary>
        /// Lista produktów
        /// </summary>
        public IReadOnlyList<Product> ProductsRO => Products.AsReadOnly();
        /// <summary>
        /// Konstruktor
        /// </summary>
        public BuyableProducts() { }
        /// <summary>
        /// Odświeża listę produktów
        /// </summary>
        public void Refresh() {
            using var db = new Shop();

            var category = db.Product_Categories;

            var products = db.Products
                .Include(a => a.Products_Prices)
                .Include(a => a.Product_Images)
                .Include(a => a.Product_Categori)
                .Where(a => a.Product_aviable);

#if DEBUG
            Console.WriteLine(products.ToQueryString());
            Console.WriteLine(category.ToQueryString());
#endif

            Products = products.ToList();
            Categoris = category.ToList();
        }
        /// <summary>
        /// Wyszukuje w liscie produktów o.
        /// </summary>
        /// <param name="categoryID">ID kategorii</param>
        /// <param name="like">Część nazwy</param>
        /// <returns>Zwraca Znależione elementy</returns>
        public List<Product> GetProducts(int? categoryID, string like = "") {
            var categoryWhere = Categoris
                .Where(a => a.Product_category_id == categoryID)
                .SelectManyRecursive(a => a.Children)
                .Select(a => a.Product_category_id).ToList();

            if(categoryID == null) {
                categoryWhere = Categoris
                    .Select(a => a.Product_category_id).ToList();
            } else
                categoryWhere.Add((int)categoryID);

            return this.Products
                .Where(a => categoryWhere.Any(b => b == a.Product_category_id))
                .Where(a => a.Product_name.ToLower().Contains(like.ToLower()))
                .ToList();
        }
    }
}
