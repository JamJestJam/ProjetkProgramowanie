using DBconnectShop.Table;
using System.Globalization;
using System.Windows.Controls;

namespace ProjektApp.Pages.productList {
    /// <summary>
    /// Interaction logic for SingleProduct_ProductsBuyPage.xaml
    /// </summary>
    public partial class SingleProduct_ProductsBuyPage : UserControl {
#pragma warning disable IDE0052 // Remove unread private members
        readonly Product Product;
#pragma warning restore IDE0052 // Remove unread private members

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
