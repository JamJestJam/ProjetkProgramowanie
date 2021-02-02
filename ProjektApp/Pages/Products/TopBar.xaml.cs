using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.Products {
    /// <summary>
    /// Interaction logic for TopBar.xaml
    /// </summary>
    public partial class TopBar : UserControl {
        ProductList productList;

        public TopBar(ProductList productList) {
            InitializeComponent();
            this.productList = productList;
        }

        private void Close(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.Close();
        }

        private void Reload(object sender, RoutedEventArgs e) {
            productList.Reload();
        }
    }
}
