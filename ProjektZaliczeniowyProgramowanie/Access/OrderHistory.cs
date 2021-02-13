using DBconnectShop.Addons;
using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop.Access {
    public class OrderHistory {
        private int ID;

        public OrderHistory(Login login) {
            int userID = 0;

            try {
                userID = login.GetUserID;
            } catch {
                throw new AuthorizationException();
            }

            this.ID = userID;
        }

        public List<User_order> GetOrderHistory() {
            using var db = new Shop();

            var history = db.User_Orders
                .Include(a => a.Products)
                .Include(a => a.Address)
                .Include(a => a.Order_Status)
                .Where(a => a.Address.User_id == ID)
                .ToList();

            return history;
        }

        public List<User_order_product> GetOrderProducts(int id) {
            using var db = new Shop();

            var products = db.User_Order_Product
                .Include(a => a.Product)
                    .ThenInclude(b => b.Product_Images)
                .Where(a => a.User_order_id == id).ToList();

            return products;
        }
    }
}
