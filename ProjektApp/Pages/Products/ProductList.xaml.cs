using DBconnectShop.Access;
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

        public BuyableProducts Products { get; }

        readonly LeftPanel left;
        readonly TopBar top;

        public int? Category { get; set; } = null;
        public string Like { get; set; } = "";

        public ProductList() {
            InitializeComponent();

            Products = new BuyableProducts();

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
            var list = Products.GetProducts(Category, Like);
            StackPanelProducts.Children.Clear();

            foreach(var ele in list) {
                UserControl tmp = new UserControl();
                tmp.Content = new SingleProduct(ele);
                StackPanelProducts.Children.Add(tmp);
            }
        }

        private void Find(object o, RoutedEventArgs e) {
            ShowProducts();
        }

        private void FindTextChange(object o, EventArgs e) {
            Like = FindText.Text;
        }
    }
}
