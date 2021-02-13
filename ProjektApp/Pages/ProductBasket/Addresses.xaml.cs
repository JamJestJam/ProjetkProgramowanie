using DBconnectShop.Access;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using LoginDB = DBconnectShop.Access.Login;

namespace ProjektApp.Pages.ProductBasket {
    /// <summary>
    /// Interaction logic for Addresses.xaml
    /// </summary>
    public partial class Addresses : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        public Addresses() {
            InitializeComponent();
            Window.TopBar.Content = new TopBar(typeof(ProductList));
            Reload();
        }

        public void Add(object o, EventArgs e) {
            Window.Loading.IsOpen = true;
            var thread = new Thread(Add) {
                IsBackground = true
            };
            thread.Start();
        }

        public void Reload() {
            Window.Loading.IsOpen = true;
            var thread = new Thread(ShowAddresses) {
                IsBackground = true
            };
            thread.Start();
        }

        public void ShowAddresses() {
            LoginDB login = null;
            Dispatcher.Invoke(() => {
                login = Window.login;
                AddressesList.Children.Clear();
            });
            UserProfil profil = new UserProfil(login);

            foreach(var address in profil.Addresses) {
                Dispatcher.Invoke(() => {
                    var control = new UserControl();
                    var single = new SingleAddress(address.User_Address_id, address.Address);
                    control.Content = single;
                    AddressesList.Children.Add(control);
                });
            }

            Dispatcher.Invoke(() => {
                Window.Loading.IsOpen = false;
            });
        }

        private void Add() {
            LoginDB login = null;
            string country = null,
                city = null,
                strret = null,
                building = null,
                zip = null;

            Dispatcher.Invoke(() => {
                login = Window.login;
                country = Country.Text;
                city = City.Text;
                strret = Street.Text;
                building = Number.Text;
                zip = ZipCode.Text;
            });

            UserProfil profil = new UserProfil(login);
            try {
                int id = profil.AddAddress(country, city, strret, building, zip);
                Dispatcher.Invoke(() => {
                    Window.DialogText.Content = "Udało się dodać adress.";
                    Window.Dialog.IsOpen = true;
                    Window.Loading.IsOpen = false;
                    Window.Content.Content = new ProductList();
                    Window.basket.Address_id = id;
                });
            } catch(Exception e) {
                Dispatcher.Invoke(() => {
                    Window.DialogText.Content = e.Message;
                    Window.Dialog.IsOpen = true;
                    Window.Loading.IsOpen = false;
                });
            }
        }
    }
}
