using DBconnectShop;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Threading;

namespace ProjektApp.Pages.productList {
    /// <summary>
    /// Interaction logic for ProductsBuyPage.xaml
    /// </summary>
    public partial class ProductsBuyPage : UserControl {
        readonly List<UserControl> controls;
        public BuyableProducts Products { get; private set; }

        ProductListLeftPage leftPage;
        ProductListTopBar topBar;

        public int Page { get; set; } = 0;
        public int PerPage { get; set; } = 18;
        public int? Category { get; set; } = null;
        public string Like { get; set; } = "";

        public ProductsBuyPage() {
            InitializeComponent();
            Products = new BuyableProducts();

            controls = new List<UserControl> {
                Product00, Product10, Product20,
                Product01, Product11, Product21,
                Product02, Product12, Product22,
                Product03, Product13, Product23
            };

            Reload();

            topBar = new ProductListTopBar(this);
            leftPage = new ProductListLeftPage(this);
            (Application.Current.MainWindow as MainWindow).TopBarr.Content = topBar;
            (Application.Current.MainWindow as MainWindow).LeftPanel.Content = leftPage;
        }

        public void Reload() {
            Hidden.IsOpen = true;

            Thread thread = new Thread(ReloadThread) {
                IsBackground = true
            };
            thread.Start();
    }

        private void ReloadThread() {
            Products.Refresh();

            Dispatcher.Invoke(()=> {
                UserPage();
                leftPage.ShowCategory();
                Hidden.IsOpen = false;
            });
        }

        public void UserPage() {
            var list = Products.GetProducts(Page, PerPage, Category, Like);

            foreach(var control in controls)
                control.Content = null;

            for(int i = 0; i < list.Count; i++)
                controls[i].Content = new SingleProduct_ProductsBuyPage(list[i]);
        }

        private void Find(object o, EventArgs e) {
            Console.WriteLine(123);
            Like = FindBox.Text;
            UserPage();
        }
    }
}
