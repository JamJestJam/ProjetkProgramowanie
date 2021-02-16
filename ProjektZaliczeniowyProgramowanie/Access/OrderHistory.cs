using DBconnectShop.Addons;
using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop.Access {
    /// <summary>
    /// Lista zamówień użytkownika
    /// </summary>
    public class OrderHistory {
        private int ID;
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="login">Autoryzacja</param>
        public OrderHistory(Login login) {
            int userID = 0;

            try {
                userID = login.GetUserID;
            } catch {
                throw new AuthorizationException();
            }

            this.ID = userID;
        }

        /// <summary>
        /// Pobiera listę zamówień
        /// </summary>
        /// <returns>Zwraca listę</returns>
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

        /// <summary>
        /// Pobiera listę produktów w zamówieniu
        /// </summary>
        /// <param name="id">ID zamówienia</param>
        /// <returns>Zwraca listę produktów</returns>
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
