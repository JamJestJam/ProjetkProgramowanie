using DBconnectShop.Access;
using DBconnectShop.Addons;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ProductListMain = ProjektApp.Pages.Products.ProductList;

namespace ProjektApp.Pages.ProductBasket {
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;
        /// <summary>
        /// Lista produktów w koszyku
        /// </summary>
        public ProductList() {
            InitializeComponent();
            GridData.ItemsSource = Values;

            Window.LeftPanel.Content = new LeftPanel(this);
            Window.TopBar.Content = new TopBar(typeof(ProductListMain));

            Window.Loading.IsOpen = true;
            Thread thread = new Thread(InsertBasket) {
                IsBackground = true
            };
            thread.Start();
        }

        internal List<Element> Values { get; set; } = new List<Element>();

        private void InsertBasket() {
            Basket basketProducts = null;
            Dispatcher.Invoke(() => {
                basketProducts = Window.basket;
            });
            var basket = new BasketProducts(basketProducts);
            basket.ShowProducts();

            int lp = 1;
            foreach(var product in basket.Products)
                Values.Add(new Element(lp++, product, GridData));

            Dispatcher.Invoke(() => {
                Window.Loading.IsOpen = false;
                (Window.LeftPanel.Content as LeftPanel)
                    .SetPrice(null, null);
            });
        }
    }

    public class Element {
        private BasketProduct ProductBS = null;
        private DataGrid Values = null;

        public string Lp { get; }
        public BitmapImage Image => ProductBS.Image.ToBitmap();
        public string Name => ProductBS.Name;
        public string Price => ProductBS.Price.ToString("0.00");
        public string Sum => ProductBS.Sum.ToString("0.00");
        public string Count {
            get => ProductBS.Count.ToString();
            set {
                if(uint.TryParse(value, out var number)) {
                    ProductBS.Count = number;
                    var tmp = Values.ItemsSource;
                    Values.ItemsSource = null;
                    Values.ItemsSource = tmp;
                }
            }
        }

        public Element(int lp, BasketProduct product, DataGrid values) {
            Lp = lp.ToString();

            ProductBS = product;
            Values = values;
        }
    }
}
