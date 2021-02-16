using DBconnectShop.Addons;
using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop.Access {
    public class AdminProducts {
        Login Login;

        public AdminProducts(Login login) {
            try {
                _ = login.GetUserID;
                if(login.Group != UserGroup.Admin)
                    throw new AuthorizationException();
            } catch {
                throw new AuthorizationException();
            }

            Login = login;
        }

        #region Product ========================================

        public List<Product> GetProducts() {
            using var db = new Shop();
            var products = db.Products
                .Include(a => a.Product_Categori)
                .Include(a => a.Product_Producer)
                .ToList();

            return products;
        }

        public List<Product_categori> GetCategories() {
            using var db = new Shop();
            var categories = db.Product_Categories
                .ToList();

            return categories;
        }

        public List<Product_producer> GetProducers() {
            using var db = new Shop();
            var producers = db.Product_Producers
                .ToList();

            return producers;
        }

        public Product NewProduct() {
            using var db = new Shop();

            Product product = new Product() {
                Product_name = "",
                Product_category_id = 1,
                Product_producer_id = 1,
                Product_aviable = false
            };

            db.Products.Add(product);
            db.SaveChanges();

            return db.Products
                    .Include(a => a.Product_Producer)
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

            product.Product_category_id =
            db.Product_Categories
                .Where(a => a.Product_category_name == value)
                .Select(a => a.Product_category_id)
                .First();
            db.SaveChanges();
        }

        public void ChangeProducer(Product product, string value) {
            using var db = new Shop();
            db.Products.Attach(product);

            product.Product_category_id =
            db.Product_Producers
                .Where(a => a.Product_producer_name == value)
                .Select(a => a.Product_producer_id)
                .First();
            db.SaveChanges();
        }

        #endregion

        #region Imgaes =========================================

        public List<Product_image> GetImages(int id) {
            using var db = new Shop();

            return db
                .Product_Images
                .Where(a => a.Product_id == id)
                .ToList();
        }

        public void ChangeActive(Product_image product, bool value) {
            using var db = new Shop();
            db.Product_Images.Attach(product);

            product.Product_image_active = value;
            db.SaveChanges();
        }

        public void ChangeImage(int id, string file) {
            using var db = new Shop();

            var image = new Image(file);
            var product = db.Product_Images.Where(a => a.Product_image_id == id).FirstOrDefault();

            db.Product_Images.Attach(product);
            product.Product_Image = image.BlobImage;

            try {
                int code = db.SaveChanges();
                if(code != 1)
                    throw new AddElementException("Wystąpił problem z przesłanym avatarem.");
            } catch {
                throw new AddElementException("Wystąpił problem z przesłanym avatarem.");
            }
        }

        public Product_image NewImage(int id) {
            using var db = new Shop();
            var tmp = Image.Default.BlobImage;

            Product_image product = new Product_image() {
                Product_image_active = false,
                Product_id = id,
                Product_Image = tmp
            };

            db.Product_Images.Add(product);
            db.SaveChanges();

            return product;
        }

        #endregion

        #region Price ==========================================

        public List<Products_price> GetPrice(int id) {
            using var db = new Shop();

            return db
                .Products_Prices
                .Where(a => a.Product_id == id)
                .ToList();
        }

        public Products_price NewPrice(int id) {
            using var db = new Shop();

            Products_price product = new Products_price() {
                Product_id = id,
                Product_price = 0,
                Product_price_date = DateTime.Now + new TimeSpan(0, 1, 0)
            };

            db.Products_Prices.Add(product);
            db.SaveChanges();

            return product;
        }

        public void ChangePrice(Products_price product, decimal value) {
            using var db = new Shop();

            if(product.Product_price_date < DateTime.Now)
                return;

            db.Products_Prices.Attach(product);
            product.Product_price = value;

            db.SaveChanges();
        }

        #endregion
    }
}
