using ProjektApp.Pages.Buttons;
using ProjektApp.Pages.Products;
using System.Windows;
using System.Windows.Controls;
using ProductListMain = ProjektApp.Pages.Products.ProductList;

namespace ProjektApp.Pages.ProductBasket {
    /// <summary>
    /// Interaction logic for TopBar.xaml
    /// </summary>
    public partial class TopBar : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        public TopBar() {
            InitializeComponent();

            UserBtn.Content = new User();
            Exit.Content = new Exit();
            Basket.Content = new Basket();
        }

        private void GoBack(object sender, RoutedEventArgs e) {
            Window.Content.Content = new ProductListMain();
        }
    }
}
