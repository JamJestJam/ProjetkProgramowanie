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
using DBconnectShop;
using ProjektApp.Pages.Products;

namespace ProjektApp.Pages.Product {
    /// <summary>
    /// Interaction logic for TopBar.xaml
    /// </summary>
    public partial class TopBar : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;
        ProductInfo productInfo;
        
        public TopBar(ProductInfo product) {
            InitializeComponent();
            productInfo = product;
        }

        private void Close(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.Close();
        }

        private void Reload(object sender, RoutedEventArgs e) {
            productInfo.Reload();
        }

        private void GoBack(object sender, RoutedEventArgs e) {
            Window.Content.Content = new ProductList();
        }
    }
}
