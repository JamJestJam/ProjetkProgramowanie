using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DBconnectShop {
    public class SingleProduct {
        private int Id { get; }

        public Product Product { get; private set; }

        public SingleProduct(int ID) {
            Id = ID;
            Reload();
        }

        public void Reload() {
            using var shop = new Shop();

            var product = shop.Products
                .Include(a => a.Products_Prices)
                .Include(a => a.Product_Images)
                .Include(a => a.Product_Specifications)
                .Include(a => a.Product_Ratings)
                .Include(a => a.Product_Opinions)
                    .ThenInclude(b => b.User)
                .Where(a => a.Product_id == Id);

#if DEBUG
            Console.WriteLine(product.ToQueryString());
#endif

            if(product.Count() != 1)
                throw new IndexOutOfRangeException();

            Product = product.First();
        }

        public Product_opinion AddComment(Login login, string content) {
            using var shop = new Shop();
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

            shop.Product_Opinions.Add(comment);
            int code;
            try {
                code = shop.SaveChanges();
            } catch {
                throw new AddElementException("Nie udało się dodać Twojej opini.\nPamiętaj można dodać tylko jedeną opinie do danego produktu.");
            }

            if(code != 1)
                throw new AddElementException("Wystąpił problem z dodanyą przez Ciebie opinią.");

            Reload();
            return Product.Product_Opinions.Where(a => a.Product_id == comment.Product_id && a.User_id == comment.User_id).First();
        }
    }

    public class AuthorizationException : Exception {
        private readonly string message = "Wystąpił problem z autoryzacją";

        public AuthorizationException() { }

        public override string Message => message;
    }

    public class AddElementException : Exception {
        private readonly string message;

        public AddElementException(string message) {
            this.message = message;
        }

        public override string Message => message;
    }
}
