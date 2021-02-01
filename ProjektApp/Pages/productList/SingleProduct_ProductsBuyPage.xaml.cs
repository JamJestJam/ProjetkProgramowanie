using DBconnectShop.Table;
using System.Globalization;
using System.Windows.Controls;

namespace ProjektApp.Pages.productList {
    /// <summary>
    /// Interaction logic for SingleProduct_ProductsBuyPage.xaml
    /// </summary>
    public partial class SingleProduct_ProductsBuyPage : UserControl {
        Product Product;

        public SingleProduct_ProductsBuyPage(Product product) {
            InitializeComponent();
            Product = product;

            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";

            ProductName.Text = product.TrueName;
            ProductPrice.Text += ' ' + product.ActualPrice.ToString("#,0.00", nfi) + "zł";
        }
    }
}
