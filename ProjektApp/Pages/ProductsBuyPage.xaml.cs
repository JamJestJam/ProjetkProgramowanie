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

namespace ProjektApp.Pages {
    /// <summary>
    /// Interaction logic for ProductsBuyPage.xaml
    /// </summary>
    public partial class ProductsBuyPage : UserControl {
        public ProductsBuyPage() {
            InitializeComponent();

            Product00.Content = new SingleProduct_ProductsBuyPage();
            Product01.Content = new SingleProduct_ProductsBuyPage();
            Product02.Content = new SingleProduct_ProductsBuyPage();
            Product03.Content = new SingleProduct_ProductsBuyPage();

            Product10.Content = new SingleProduct_ProductsBuyPage();
            Product11.Content = new SingleProduct_ProductsBuyPage();
            Product12.Content = new SingleProduct_ProductsBuyPage();
            Product13.Content = new SingleProduct_ProductsBuyPage();

            Product20.Content = new SingleProduct_ProductsBuyPage();
            Product21.Content = new SingleProduct_ProductsBuyPage();
            Product22.Content = new SingleProduct_ProductsBuyPage();
            Product23.Content = new SingleProduct_ProductsBuyPage();
        }
    }
}
