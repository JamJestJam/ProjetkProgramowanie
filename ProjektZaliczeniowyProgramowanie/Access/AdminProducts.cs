using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBconnectShop.Addons;
using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;

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

        public List<Product> GetProducts() {
            using var db = new Shop();
            var products = db.Products
                .Include(a=>a.Product_Categori)
                .ToList();

            return products;
        }

        public List<Product_categori> GetCategories() {
            using var db = new Shop();
            var categories = db.Product_Categories
                .ToList();

            return categories;
        }
    }
}
