using ProjektApp.Pages.Buttons;
using ProjektApp.Pages.Products;
using System.Windows;
using System.Windows.Controls;

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

            UserBtn.Content = new User();
            Exit.Content = new Exit();
            Basket.Content = new Basket();
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
