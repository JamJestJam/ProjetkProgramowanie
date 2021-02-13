using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using DBconnectShop.Table;

namespace ProjektApp.Pages.ProductBasket {
    /// <summary>
    /// Interaction logic for SingleAddress.xaml
    /// </summary>
    public partial class SingleAddress : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;
        int ID;

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
