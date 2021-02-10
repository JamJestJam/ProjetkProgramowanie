using ProjektApp.Pages.Product;
using System.Windows;
using System.Windows.Controls;
using ProductTable = DBconnectShop.Table.Product;

namespace ProjektApp.Pages.Products {
    /// <summary>
    /// Interaction logic for SingleProduct.xaml
    /// </summary>
    public partial class SingleProduct : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        private readonly ProductTable product;

        public SingleProduct(ProductTable product) {
            InitializeComponent();
            this.product = product;

            ManageInfo();
        }

        private void ManageInfo() {
            ProductName.Text = product.TrueName;
            ProductPrice.Text += ' ' + product.ActualPrice.ToString("#,0.00") + "zł";
        }

        private void ShowMore(object o, RoutedEventArgs e) {
            Window.Content.Content = new ProductInfo(product.ID);
        }

        private void AddBasket(object o, RoutedEventArgs e) {
            Window.basket.AddProduct(product.ID);
        }
    }
}
