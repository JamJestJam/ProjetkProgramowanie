using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop.Access {
    public partial class AdminControl {
        /// <summary>
        /// Pobiera liste produktów
        /// </summary>
        /// <returns>Zwraca listę produktów</returns>
        public List<Product> GetProducts() {
            using var db = new Shop();
            var products = db.Products
                .Include(a => a.Product_Categori)
                .ToList();

            return products;
        }

        /// <summary>
        /// Pobiera liste kategorii
        /// </summary>
        /// <returns>Zwraca listę kategorii</returns>
        public List<Product_categori> GetCategories() {
            using var db = new Shop();
            var categories = db.Product_Categories
                .ToList();

            return categories;
        }

        /// <summary>
        /// Tworzy nowy produkt
        /// </summary>
        /// <returns>Zwraca stworzony produkt</returns>
        public Product NewProduct() {
            using var db = new Shop();

            Product product = new Product() {
                Product_name = "",
                Product_category_id = 1,
                Product_aviable = false
            };

            db.Products.Add(product);
            db.SaveChanges();

            return db.Products
                    .Include(a => a.Product_Categori)
                    .First(a => a.Product_id == product.ID);
        }

        /// <summary>
        /// Zmienia nazwe produktu
        /// </summary>
        /// <param name="product">Produkt do zmiany</param>
        /// <param name="value">Wartość jaką ma przyjąć</param>
        public void ChangeName(Product product, string value) {
            using var db = new Shop();
            db.Products.Attach(product);

            product.Product_name = value;
            db.SaveChanges();
        }

        /// <summary>
        /// Zmienia stan produktu
        /// </summary>
        /// <param name="product">Produkt do zmiany</param>
        /// <param name="value">Stan</param>
        public void ChangeAviable(Product product, bool value) {
            using var db = new Shop();
            db.Products.Attach(product);

            product.Product_aviable = value;
            db.SaveChanges();
        }

        /// <summary>
        /// Zmienia kategorie produktu
        /// </summary>
        /// <param name="product">Produkt do zmiany</param>
        /// <param name="value">nazwa kategorii</param>
        public void ChangeCategory(Product product, string value) {
            using var db = new Shop();
            db.Products.Attach(product);

            product.Product_Categori =
            db.Product_Categories
                .Where(a => a.Product_category_name == value)
                .First();
            db.SaveChanges();
        }
    }
}
