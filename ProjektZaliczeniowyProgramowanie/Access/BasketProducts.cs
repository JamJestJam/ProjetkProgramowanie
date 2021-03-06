﻿using DBconnectShop.Addons;
using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop.Access {
    /// <summary>
    /// Koszyk
    /// </summary>
    public class BasketProducts {
        Basket Basket { get; }
        /// <summary>
        /// Lista produktów w koszyku
        /// </summary>
        public List<BasketProduct> Products { get; private set; }
        /// <summary>
        /// tworzy nowy koszyk
        /// </summary>
        /// <param name="basket">Lista zakupów</param>
        public BasketProducts(Basket basket) {
            Basket = basket;
        }
        /// <summary>
        /// Zamienia listę zakupów na produkty
        /// </summary>
        public void ShowProducts() {
            using var db = new Shop();

            var productsQuery = db.Products
                .Include(a => a.Products_Prices)
                .Include(a => a.Product_Images)
                .Where(a => Basket.ProductList.Keys.Any(b => b == a.Product_id));

#if DEBUG
            Console.WriteLine(productsQuery.ToQueryString());
#endif

            Products = new List<BasketProduct>();
            foreach(var product in productsQuery)
                Products.Add(new BasketProduct(Basket, product));
        }
        /// <summary>
        /// Kupuje produktu
        /// </summary>
        /// <param name="profil">Użytkownik który kupuje</param>
        public void Buy(UserProfil profil) {
            if(profil.FirstName == "" || profil.FamilyName == "")
                throw new ArgumentException("Przez zamówieniem produktów uzupełnij profil.");
            if(profil.Address.Count == 0)
                throw new ArgumentException("Przez zamówieniem produktów wpisz adres przesyłki.");
            if(Basket.Address_id is null)
                throw new ArgumentException("Nie wybrałeś jeszcze żadnego adresu.");
            if(profil.Addresses.Where(a => a.User_Address_id == Basket.Address_id).Count() == 0)
                throw new ArgumentException("Wybrany adress nie jest poprawny.");

            using var db = new Shop();
            var products = new List<User_order_product>();
            foreach(var item in Basket.ProductList) {
                var productID = item.Key;
                var productCount = item.Value;
                var product = db.Products
                    .Include(a => a.Products_Prices)
                    .FirstOrDefault(a => a.Product_id == productID);

                if(product != null) {
                    for(int i = 0; i < productCount; i++) {
                        products.Add(new User_order_product() {
                            Product_id = productID,
                            User_order_Product_price = product.ActualPrice
                        });
                    }
                }
            }

            var order = new User_order() {
                User_order_status_id = 1,
                User_Address_id = (int)Basket.Address_id,
                Products = products
            };

            db.User_Orders.Add(order);
            int code = db.SaveChanges();
            if(code == 0)
                throw new AddElementException("Zamówienie koszyka nie powiodło się");
        }
    }

    /// <summary>
    /// Produkt w koszyku
    /// </summary>
    public class BasketProduct {
        Basket Basket { get; }
        Product Product { get; }
        /// <summary>
        /// Ilość danego produktu
        /// </summary>
        public uint Count {
            get => Basket.ProductList[Product.ID];
            set => Basket.SetCount(Product.ID, value);
        }
        /// <summary>
        /// Nazwa produktu
        /// </summary>
        public string Name =>
            Product.TrueName;
        /// <summary>
        /// Cena produktu
        /// </summary>
        public decimal Price =>
            Product.ActualPrice;
        /// <summary>
        /// Całkowita cena produktów
        /// </summary>
        public decimal Sum =>
            Price * Count;
        /// <summary>
        /// Miniaturka
        /// </summary>
        public Image Image =>
            Product.FirstImage;

        internal BasketProduct(Basket basket, Product product) {
            Basket = basket;
            Product = product;
        }
    }
}
