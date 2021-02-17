using DBconnectShop.Table;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.ProductBasket {
    /// <summary>
    /// Interaction logic for SingleAddress.xaml
    /// </summary>
    public partial class SingleAddress : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;
        int ID;

        /// <summary>
        /// Pojedynczy address w liscie addresów
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="address">Addres</param>
        public SingleAddress(int id, Address address) {
            InitializeComponent();

            ID = id;
            Country.Text = address.Country;
            City.Text = address.City;
            Street.Text = address.Street;
            Number.Text = address.BuildingNumber;
            ZipCode.Text = address.ZipCode;
        }

        private void UseID(object o, EventArgs e) {
            Window.basket.Address_id = ID;
            Window.Content.Content = new ProductList();
            Window.Dialog.IsOpen = true;
            Window.DialogText.Content = "Wybrałeś address";
        }
    }
}
