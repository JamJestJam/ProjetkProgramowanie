using DBconnectShop.Access;
using DBconnectShop.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ProjektApp.Pages.Admin.Product {
    /// <summary>
    /// Interaction logic for ProductSpecyfication.xaml
    /// </summary>
    public partial class ProductSpecyfication : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;
        AdminProducts admin = new AdminProducts(Window.login);
        public static int ID { get; private set; }
        List<Element> Values { get; set; } = new List<Element>();

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
            AdminProducts admin = new AdminProducts(Window.login);

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
