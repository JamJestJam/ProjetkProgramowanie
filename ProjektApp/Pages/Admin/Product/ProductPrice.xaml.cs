using DBconnectShop.Access;
using DBconnectShop.Table;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.Admin.Product {
    /// <summary>
    /// Interaction logic for ProductPrice.xaml
    /// </summary>
    public partial class ProductPrice : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;
        AdminControl admin = new AdminControl(Window.login);
        public static int ID { get; private set; }
        List<Element> Values { get; set; } = new List<Element>();


        public ProductPrice(int id) {
            InitializeComponent();
            ID = id;

            Window.Loading.IsOpen = true;
            Thread thread = new Thread(InitItems) {
                IsBackground = true
            };
            thread.Start();
        }

        private void InitItems() {
            foreach(var product in admin.GetPrice(ID)) {
                Dispatcher.Invoke(() => {
                    Values.Add(new Element(product));
                });
            }

            Dispatcher.Invoke(() => {
                Window.Loading.IsOpen = false;
                GridData.ItemsSource = Values;
            });
        }

        class Element {
            static MainWindow Window =>
                            Application.Current.MainWindow as MainWindow;
            AdminControl admin = new AdminControl(Window.login);

            Products_price Product { get; }
            public decimal Price {
                get => Product.Product_price;
                set => admin.ChangePrice(Product, value);
            }
            public string Date =>
                Product.Product_price_date.ToString();

            public Element() {
                Product = admin.NewPrice(ID);
            }

            public Element(Products_price price) {
                Product = price;
            }
        }
    }
}
