using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using DBLogin = DBconnectShop.Access.Login;
using DBOrderHistory = DBconnectShop.Access.OrderHistory;

namespace ProjektApp.Pages.Profile {
    /// <summary>
    /// Interaction logic for ProductsInOrder.xaml
    /// </summary>
    public partial class ProductsInOrder : UserControl {
        static MainWindow Window =>
                Application.Current.MainWindow as MainWindow;
        int ID;

        public ProductsInOrder(int id) {
            InitializeComponent();

            ID = id;
            Window.Loading.IsOpen = true;
            Thread thread = new Thread(ShowProducts) {
                IsBackground = true
            };
            thread.Start();
        }

        private void ShowProducts() {
            DBLogin login = null;
            int id = 0;
            Dispatcher.Invoke(() => {
                login = Window.login;
                id = ID;
            });
            var history = new DBOrderHistory(login);
            var products = history.GetOrderProducts(id)
                .GroupBy(a => a.Product_id)
                .Select(a => new {
                    count = a.Count(),
                    product = a.First().Product,
                    Price = a.First().User_order_Product_price
                }).ToList();
            int lp = 1;

            foreach(var product in products) {
                Dispatcher.Invoke(() => {
                    GridData.Items.Add(new {
                        c1 = lp++,
                        c2 = product.product.TrueName,
                        c3 = product.Price.ToString("#0.00"),
                        c4 = product.count
                    });
                });
            }
            Dispatcher.Invoke(() => {
                Window.Loading.IsOpen = false;
            });
        }
    }
}
