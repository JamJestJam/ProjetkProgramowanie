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
using DBconnectShop.Access;
using ProductDB = DBconnectShop.Table.Product;
using LoginDB = DBconnectShop.Access.Login;

namespace ProjektApp.Pages.Admin.Product {
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        public List<Element> Values { get; set; } = new List<Element>();
        public List<string> Categories { get; set; } = new List<string>();

        public ProductList() {
            InitializeComponent();

            Window.Loading.IsOpen = true;
            Thread thread = new Thread(InitItems) {
                IsBackground = true
            };
            thread.Start();
        }

        private void InitItems() {
            LoginDB login = null;
            Dispatcher.Invoke(() => {
                login = Window.login;
            });

            AdminProducts admin = new AdminProducts(login);
            Categories = admin.GetCategories().Select(a => a.TrueName).ToList();
            Dispatcher.Invoke(() => {
                Categoriee.ItemsSource = Categories;
            });


            foreach(var product in admin.GetProducts()) {
                Dispatcher.Invoke(() => {
                    Values.Add(new Element(product));
                });
            }

            Dispatcher.Invoke(() => {
                Window.Loading.IsOpen = false;
                GridData.ItemsSource = Values;
            });
        }
    }

    public class Element {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; } 
        public bool Aviable { get; set; }

        public Element(ProductDB product) {
            ID = product.ID;
            Name = product.TrueName;
            Aviable = product.Product_aviable;
            Category = product.Product_Categori.TrueName;
        }

        public Element() {
            ID = 0;
            Name = "";
            Aviable = false;
            Category = "Przyklad";
        }
    }
}
