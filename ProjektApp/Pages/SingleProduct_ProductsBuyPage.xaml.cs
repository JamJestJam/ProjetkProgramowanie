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

namespace ProjektApp.Pages {
    /// <summary>
    /// Interaction logic for SingleProduct_ProductsBuyPage.xaml
    /// </summary>
    public partial class SingleProduct_ProductsBuyPage : UserControl {
        public SingleProduct_ProductsBuyPage(string productName, decimal productPrice) {
            InitializeComponent();
            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";

            ProductName.Text = productName;
            ProductPrice.Text += ' ' + productPrice.ToString("#,0.00", nfi) + "zł";
        }
    }
}
