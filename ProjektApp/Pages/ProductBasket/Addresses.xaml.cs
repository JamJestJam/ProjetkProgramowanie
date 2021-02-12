using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DBconnectShop.Access;
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
        }

        public void Add(object o, EventArgs e) {
            Window.Loading.IsOpen = true;
            var thread = new Thread(Add) {
                IsBackground = true
            };
            thread.Start();
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
                profil.AddAddress(country, city, strret, building, zip);
            }catch(Exception e) {
                Dispatcher.Invoke(() => {
                    Window.DialogText.Content = e.Message;
                    Window.Dialog.IsOpen = true;
                    Window.Loading.IsOpen = false;
                });
            }
        }
    }
}
