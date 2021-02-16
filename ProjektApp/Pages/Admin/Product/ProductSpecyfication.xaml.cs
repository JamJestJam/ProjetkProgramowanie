using DBconnectShop.Access;
using DBconnectShop.Table;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.Admin.Product {
    /// <summary>
    /// Interaction logic for ProductSpecyfication.xaml
    /// </summary>
    public partial class ProductSpecyfication : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;
        private AdminControl admin = new AdminControl(Window.login);
        private static int ID { get; set; }
        private List<Element> Values { get; set; } = new List<Element>();

        public ProductSpecyfication(int id) {
            InitializeComponent();
            ID = id;

            Window.Loading.IsOpen = true;
            Thread thread = new Thread(InitItems) {
                IsBackground = true
            };
            thread.Start();
        }

        private void InitItems() {
            foreach(var product in admin.GetSpecyfication(ID)) {
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

            Product_specification Product { get; }

            public int Id =>
                Product.ID;
            public string Name {
                get => Product.Name;
                set => admin.ChangeName(Product, value);
            }
            public string Value {
                get => Product.Value;
                set => admin.ChangeValue(Product, value);
            }

            public Element() {
                Product = admin.NewSpecyfication(ID);
            }

            public Element(Product_specification product) {
                Product = product;
            }
        }
    }
}
