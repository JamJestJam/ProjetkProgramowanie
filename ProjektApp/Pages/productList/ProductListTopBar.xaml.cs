using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.productList {
    /// <summary>
    /// Interaction logic for TopBarLogged.xaml
    /// </summary>
    public partial class ProductListTopBar : UserControl {
        ProductsBuyPage Page;

        public ProductListTopBar(ProductsBuyPage page) {
            InitializeComponent();
            Page = page;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            Page.Reload();
        }
    }
}
