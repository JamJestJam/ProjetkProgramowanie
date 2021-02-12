using DBconnectShop.Access;
using Microsoft.Win32;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace ProjektApp.Pages.ProductBasket {
    /// <summary>
    /// Interaction logic for LeftPanel.xaml
    /// </summary>
    public partial class LeftPanel : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;
        ProductList list;

        public LeftPanel(ProductList list) {
            InitializeComponent();
            this.list = list;
            SetPrice(null, null);

            Window.basket.OnChange += SetPrice;
        }

        public void SetPrice(object o, EventArgs e) {
            var sum = list.Values.Sum(a=>decimal.Parse(a.Sum));
            Price.Text = sum.ToString("#,0.00") + "zł";
        }
    }
}
