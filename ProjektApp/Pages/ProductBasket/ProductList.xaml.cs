using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using DBconnectShop.Access;
using DBconnectShop.Addons;

namespace ProjektApp.Pages.ProductBasket {
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        public ProductList() {
            InitializeComponent();
            GridData.ItemsSource = Values;

            Window.Loading.IsOpen = true;
            Thread thread = new Thread(InsertBasket) {
                IsBackground = true
            };
            thread.Start();
        }

        List<Element> Values { get; set; } = new List<Element>();

        private void InsertBasket() {
            Basket basketProducts = null;
            Dispatcher.Invoke(() => {
                basketProducts = Window.basket;
            });
            var basket = new BasketProducts(basketProducts); ;

            int lp = 0;
            foreach(var product in basket.Products)
                Values.Add(new Element(lp++, product, GridData));

            Dispatcher.Invoke(() => {
                Window.Loading.IsOpen = false;
            });
        }
    }

    class Element {
        static MainWindow Window =>
                Application.Current.MainWindow as MainWindow;
        private BasketProduct ProductBS = null;
        private DataGrid Values = null;

        public string Lp { get; }
        public string Image => ProductBS.Image;
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
