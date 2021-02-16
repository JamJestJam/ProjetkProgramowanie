using DBconnectShop.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop.Access {
    partial class Admin {
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
    }
}
