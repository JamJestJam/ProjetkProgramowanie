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
    /// Interaction logic for ProductCategories.xaml
    /// </summary>
    public partial class ProductCategories : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;
        AdminControl admin = new AdminControl(Window.login);
        List<Element> Values { get; set; } = new List<Element>();
        List<string> Categories { get; set; } = new List<string>();

        public ProductCategories() {
            InitializeComponent();

            Window.Loading.IsOpen = true;
            Thread thread = new Thread(InitItems) {
                IsBackground = true
            };
            thread.Start();
        }

        private void InitItems() {
            Categories = admin
                .GetCategories()
                .Select(a => a.TrueName)
                .Union(new string[] { "Null" })
                .ToList();

            foreach(var product in admin.GetCategories()) {
                Dispatcher.Invoke(() => {
                    Values.Add(new Element(product));
                });
            }

            Dispatcher.Invoke(() => {
                Window.Loading.IsOpen = false;
                GridData.ItemsSource = Values;
                Categoriee.ItemsSource = Categories;
            });
        }

        class Element {
            static MainWindow Window =>
                            Application.Current.MainWindow as MainWindow;
            AdminControl admin = new AdminControl(Window.login);
            Product_categori Product { get; }

            public int ID =>
                Product.ID;
            public string Name {
                get => Product.TrueName;
                set => admin.ChangeCategoryName(Product, value);
            }
            public string Parent {
                get => (Product.Parent is null) ? "Null" : Product.Parent.TrueName;
                set => admin.ChangeParent(Product, value);
            }

            public Element() {
                Product = admin.NewCategori();
            }

            public Element(Product_categori product) {
                Product = product;
            }
        }
    }
}
