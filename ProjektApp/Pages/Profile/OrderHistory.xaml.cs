using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using DBLogin = DBconnectShop.Access.Login;
using DBOrderHistory = DBconnectShop.Access.OrderHistory;

namespace ProjektApp.Pages.Profile {
    /// <summary>
    /// Interaction logic for OrderHistory.xaml
    /// </summary>
    public partial class OrderHistory : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        public OrderHistory() {
            InitializeComponent();

            Window.Loading.IsOpen = true;
            Thread thread = new Thread(ShowProducts) {
                IsBackground = true
            };
            thread.Start();
        }

        private void ShowProducts() {
            DBLogin login = null;
            Dispatcher.Invoke(() => {
                login = Window.login;
            });
            var history = new DBOrderHistory(login);
            int id = 0;

            foreach(var order in history.GetOrderHistory()) {
                Dispatcher.Invoke(() => {
                    GridData.Items.Add(
                        new {
                            c1 = id++,
                            c2 = order.Order_Status.User_order_status_name,
                            c3 = order.User_order_date.ToString("yyyy-MM-dd\nHH-mm-ss"),
                            c4 = order.Products.Count().ToString(),
                            c5 = order.User_order_id
                        });
                });
            }
            Dispatcher.Invoke(() => {
                Window.Loading.IsOpen = false;
            });
        }

        private void ShowProduct(object o, EventArgs e) {
            Window.Content.Content = new ProductsInOrder(
                (int)((Button)o).CommandParameter);
        }
    }
}
