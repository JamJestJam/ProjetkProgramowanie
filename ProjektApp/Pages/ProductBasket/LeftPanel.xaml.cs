using DBconnectShop.Access;
using DBconnectShop.Addons;
using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using LoginDB = DBconnectShop.Access.Login;


namespace ProjektApp.Pages.ProductBasket {
    /// <summary>
    /// Interaction logic for LeftPanel.xaml
    /// </summary>
    public partial class LeftPanel : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;
        ProductList list;
        /// <summary>
        /// Lewy panel dla listy zakupów
        /// </summary>
        /// <param name="list">Lista produktów</param>
        public LeftPanel(ProductList list) {
            InitializeComponent();
            this.list = list;
            SetPrice(null, null);

            Window.basket.OnChange += SetPrice;
        }

        internal void SetPrice(object o, EventArgs e) {
            var sum = list.Values.Sum(a => decimal.Parse(a.Sum));
            Price.Text = sum.ToString("#,0.00") + "zł";
        }

        private void Buy(object o, EventArgs e) {
            Window.Loading.IsOpen = true;

            Thread thread = new Thread(Buy) {
                IsBackground = true
            };
            thread.Start();
        }

        private void Buy() {
            LoginDB login = null;
            Basket basket = null;
            Dispatcher.Invoke(() => {
                login = Window.login;
                basket = Window.basket;
            });

            var profil = new UserProfil(login);

            try {
                var buy = new BasketProducts(basket);
                buy.Buy(profil);

                Dispatcher.Invoke(() => {
                    Window.basket = new Basket();
                    Window.Dialog.IsOpen = true;
                    Window.DialogText.Content = "Wysłano zamówienie";
                    Window.Loading.IsOpen = false;
                    Window.Content.Content = new ProductList();
                });
            } catch(Exception e) {
                Dispatcher.Invoke(() => {
                    Window.Dialog.IsOpen = true;
                    Window.DialogText.Content = e.Message;
                    Window.Loading.IsOpen = false;
                });
            }
        }

        private void Address(object o, EventArgs e) {
            Window.Content.Content = new Addresses();
        }
    }
}
