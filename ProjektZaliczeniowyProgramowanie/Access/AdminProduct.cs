using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop.Access {
    public partial class AdminControl {
        public List<Product> GetProducts() {
            using var db = new Shop();
            var products = db.Products
                .Include(a => a.Product_Categori)
                .ToList();

            return products;
        }

        public List<Product_categori> GetCategories() {
            using var db = new Shop();
            var categories = db.Product_Categories
                .ToList();

            return categories;
        }

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

        public void ChangeName(Product product, string value) {
            using var db = new Shop();
            db.Products.Attach(product);

            product.Product_name = value;
            db.SaveChanges();
        }

        public void ChangeAviable(Product product, bool value) {
            using var db = new Shop();
            db.Products.Attach(product);

            product.Product_aviable = value;
            db.SaveChanges();
        }

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
