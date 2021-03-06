﻿using DBconnectShop.Addons;
using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DBconnectShop.Access {
    /// <summary>
    /// Pojedynczy produkt
    /// </summary>
    public class SingleProduct {
        private int Id { get; }
        /// <summary>
        /// Produkt
        /// </summary>
        public Product Product { get; private set; } = null;
        /// <summary>
        /// Tworzy nową listę
        /// </summary>
        /// <param name="ID">ID produktu</param>
        public SingleProduct(int ID) {
            Id = ID;
            Reload();
        }

        /// <summary>
        /// Odświeża informacje o produkcie
        /// </summary>
        public void Reload() {
            using var shop = new Shop();

            var product = shop.Products
                .Include(a => a.Products_Prices)
                .Include(a => a.Product_Images)
                .Include(a => a.Product_Specifications)
                .Include(a => a.Product_Ratings)
                .Include(a => a.Product_Opinions)
                    .ThenInclude(b => b.User)
                        .ThenInclude(b => b.User_Data)
                .Where(a => a.Product_id == Id);

#if DEBUG
            Console.WriteLine(product.ToQueryString());
#endif

            if(product.Count() != 1)
                throw new IndexOutOfRangeException();

            Product = product.First();
        }

        /// <summary>
        /// Pobiera średni ranking produktu
        /// </summary>
        public short AverageRating {
            get {
                if(Product.Product_Ratings.Count() == 0)
                    return 0;

                return (short)Product.Product_Ratings.Average(a => a.Product_Rating);
            }
        }

        /// <summary>
        /// Dodaje komentarz pod produktem
        /// </summary>
        /// <param name="login">Autoryzacja</param>
        /// <param name="content">Tresc komentarza</param>
        /// <returns>Zwraca wysłany komentarz</returns>
        public Product_opinion AddComment(Login login, string content) {
            using var db = new Shop();
            int userID = -1;
            int prodctID = -1;

            try {
                userID = login.GetUserID;
                prodctID = Product.ID;
            } catch {
                throw new AuthorizationException();
            }

            var comment = new Product_opinion() {
                User_id = userID,
                Product_id = prodctID,
                Product_Opinion = content
            };

            db.Product_Opinions.Add(comment);
            int code;
            try {
                code = db.SaveChanges();
            } catch {
                throw new AddElementException("Nie udało się dodać Twojej opini.");
            }

            if(code != 1)
                throw new AddElementException("Wystąpił problem z dodaną przez Ciebie opinią.");

            Reload();
            return Product.Product_Opinions
                .LastOrDefault(a => a.Product_id == comment.Product_id && a.User_id == comment.User_id);
        }

        /// <summary>
        /// Dodaje ocenę produktu
        /// </summary>
        /// <param name="login">Autoryzacja</param>
        /// <param name="rate">Ocena</param>
        public void AddRate(Login login, short rate) {
            using var db = new Shop();
            int userID = 0;
            int productID = 0;

            try {
                userID = login.GetUserID;
                productID = Product.ID;
            } catch {
                throw new AuthorizationException();
            }

            Product_rating rating = db.Product_Ratings
                .FirstOrDefault(a => a.User_id == userID && a.Product_id == productID);

            if(rating is null) {
                rating = new Product_rating() {
                    User_id = userID,
                    Product_id = productID,
                    Product_Rating = rate
                };
                db.Product_Ratings.Add(rating);
            } else {
                rating.Product_Rating = rate;
            }

            int code = db.SaveChanges();
            if(code != 1)
                throw new AddElementException("Wystąpił problem z dodaną przez Ciebie opinią.");

            Reload();
        }

        /// <summary>
        /// Pobiera poprzednią ocene użytkownika
        /// </summary>
        /// <param name="login">Autoryzacja</param>
        /// <returns>Ocena użytkownika</returns>
        public short GetUserRate(Login login) {
            int userID = 0;
            int productID = 0;

            try {
                userID = login.GetUserID;
                productID = Id;
            } catch {
                throw new AuthorizationException();
            }

            return Product.Product_Ratings
                .Where(a => a.User_id == userID && a.Product_id == productID)
                .Select(a => a.Product_Rating)
                .FirstOrDefault();
        }
    }
}
