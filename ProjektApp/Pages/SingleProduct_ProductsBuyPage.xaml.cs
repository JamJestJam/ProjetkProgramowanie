using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace ProjektApp.Pages {
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
