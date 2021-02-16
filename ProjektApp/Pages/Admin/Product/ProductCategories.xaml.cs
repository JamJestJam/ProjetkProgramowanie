using DBconnectShop.Access;
using DBconnectShop.Table;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

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

        /// <summary>
        /// Clasa obsługująca datagrid
        /// </summary>
        private class Element {
            static MainWindow Window =>
                            Application.Current.MainWindow as MainWindow;
            AdminControl admin = new AdminControl(Window.login);
            Product_categori Product { get; }

            /// <summary>
            /// ID kategori
            /// </summary>
            public int ID =>
                Product.ID;
            /// <summary>
            /// Nazwa kategorii
            /// </summary>
            public string Name {
                get => Product.TrueName;
                set => admin.ChangeCategoryName(Product, value);
            }
            /// <summary>
            /// Rodzic kategorii
            /// </summary>
            public string Parent {
                get => (Product.Parent is null) ? "Null" : Product.Parent.TrueName;
                set => admin.ChangeParent(Product, value);
            }
            /// <summary>
            /// Tworzy nowy wiersz w datarowie
            /// </summary>
            public Element() {
                Product = admin.NewCategori();
            }
            /// <summary>
            /// Tworzy nowy wiersz w datarowie
            /// </summary>
            /// <param name="product">Kategoria którą ma być wypełniony</param>
            public Element(Product_categori product) {
                Product = product;
            }
        }
    }
}
