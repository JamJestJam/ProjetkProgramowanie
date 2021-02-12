using DBconnectShop.Access;
using Microsoft.Win32;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using LoginDB = DBconnectShop.Access.Login;

namespace ProjektApp.Pages.ProductBasket {
    /// <summary>
    /// Interaction logic for LeftPanel.xaml
    /// </summary>
    public partial class LeftPanel : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;
        ProductList list;

        public LeftPanel(ProductList list) {
            InitializeComponent();
            this.list = list;
            SetPrice(null, null);

            Window.basket.OnChange += SetPrice;
        }

        public void SetPrice(object o, EventArgs e) {
            var sum = list.Values.Sum(a => decimal.Parse(a.Sum));
            Price.Text = sum.ToString("#,0.00") + "zł";
        }

        public void Buy(object o, EventArgs e) {
            Window.Loading.IsOpen = true;

            Thread thread = new Thread(Buy) {
                IsBackground = true
            };
            thread.Start();
        }

        private void Buy() {
            LoginDB login = null;
            Dispatcher.Invoke(() => {
                login = Window.login;
            });

            var profil = new UserProfil(login);

            if(profil.FirstName == "" || profil.FamilyName == "") {
                Dispatcher.Invoke(() => {
                    Window.DialogText.Content = "Przez zamówieniem produktów uzupełnij profil.";
                    Window.Dialog.IsOpen = true;
                    Window.Loading.IsOpen = false;
                });
            }else if(profil.Address.Count == 0){
                Dispatcher.Invoke(() => {
                    Window.DialogText.Content = "Przez zamówieniem produktów wpisz adres przesyłki.";
                    Window.Dialog.IsOpen = true;
                    Window.Loading.IsOpen = false;
                });
            } else {
                Dispatcher.Invoke(() => {
                    Window.DialogText.Content = "Jeszcze nie działam.";
                    Window.Dialog.IsOpen = true;
                    Window.Loading.IsOpen = false;
                });
            }
        }

        private void Address(object o, EventArgs e) {
            Window.Content.Content = new Addresses();
        }
    }
}
