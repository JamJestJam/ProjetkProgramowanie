using DBconnectShop.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop.Access {
    public partial class AdminControl {
        /// <summary>
        /// Pobiera liste historii cen produktu
        /// </summary>
        /// <param name="id">ID produktu</param>
        /// <returns>lista cen produktu</returns>
        public List<Products_price> GetPrice(int id) {
            using var db = new Shop();

            return db
                .Products_Prices
                .Where(a => a.Product_id == id)
                .ToList();
        }

        /// <summary>
        /// Dodaje nową cene produktu
        /// </summary>
        /// <param name="id">ID produktu</param>
        /// <returns>Zwraca dodaną cene</returns>
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

        /// <summary>
        /// Zmienia cene produktu w czasie nie dłuższym niż minuta od jej dodania
        /// </summary>
        /// <param name="product">Cena produktu do zmiany</param>
        /// <param name="value">Wartość na jaką zmienia</param>
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
