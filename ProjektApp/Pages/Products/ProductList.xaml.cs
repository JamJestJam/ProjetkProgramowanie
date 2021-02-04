using DBconnectShop;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.Products {
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        readonly List<UserControl> controls;
        public BuyableProducts Products { get; }

        readonly LeftPanel left;
        readonly TopBar top;

        public int Page { get; set; } = 0;
        public int PerPage { get; set; } = 18;
        public int? Category { get; set; } = null;
        public string Like { get; set; } = "";

        public ProductList() {
            InitializeComponent();

            Products = new BuyableProducts();
            controls = new List<UserControl> {
                Product00, Product10, Product20,
                Product01, Product11, Product21,
                Product02, Product12, Product22,
                Product03, Product13, Product23
            };

            left = new LeftPanel(this);
            top = new TopBar(this);
            Window.LeftPanel.Content = left;
            Window.TopBar.Content = top;

            Reload();
        }

        public void Reload() {
            Window.Loading.IsOpen = true;

            Thread thread = new Thread(ReloadContent) {
                IsBackground = true
            };
            thread.Start();
        }

        public void ReloadContent() {
            Products.Refresh();

            Dispatcher.Invoke(() => {
                ShowProducts();
                left.ShowCategory();
                Window.Loading.IsOpen = false;
            });
        }

        public void ShowProducts() {
            var list = Products.GetProducts(Page, PerPage, Category, Like);
            controls.ForEach(a => a.Content = null);

            for(int i = 0; i < list.Count; i++)
                controls[i].Content = new SingleProduct(list[i]);
        }

        private void Find(object o, RoutedEventArgs e) {
            ShowProducts();
        }

        private void FindTextChange(object o, EventArgs e) {
            Like = FindText.Text;
        }
    }
}
