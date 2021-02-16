using DBconnectShop.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using ProductDB = DBconnectShop.Table.Product;


namespace ProjektApp.Pages.Admin.Product {
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;
        AdminControl admin = new AdminControl(Window.login);

        List<Element> Values { get; set; } = new List<Element>();
        List<string> Categories { get; set; } = new List<string>();

        public ProductList() {
            InitializeComponent();

            Window.Loading.IsOpen = true;
            Thread thread = new Thread(InitItems) {
                IsBackground = true
            };
            thread.Start();
        }

        private void InitItems() {
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

        private void ShowImages(object o, EventArgs e) {
            if(((Button)o).CommandParameter is null)
                return;
            var id = (int)((Button)o).CommandParameter;
            Window.Content.Content = new ProductImages(id);
        }

        private void ShowPrice(object o, EventArgs e) {
            if(((Button)o).CommandParameter is null)
                return;
            var id = (int)((Button)o).CommandParameter;
            Window.Content.Content = new ProductPrice(id);
        }

        private void ShowSpecyfication(object o, EventArgs e) {
            if(((Button)o).CommandParameter is null)
                return;
            var id = (int)((Button)o).CommandParameter;
            Window.Content.Content = new ProductSpecyfication(id);
        }

        class Element {
            static MainWindow Window =>
                Application.Current.MainWindow as MainWindow;
            AdminControl admin = new AdminControl(Window.login);

            public int ID =>
                Product.ID;
            public string Name {
                get => Product.TrueName;
                set => admin.ChangeName(Product, value);
            }
            public string Category {
                get => Product.Product_Categori.TrueName;
                set => admin.ChangeCategory(Product, value);
            }
            public bool Aviable {
                get => Product.Product_aviable;
                set => admin.ChangeAviable(Product, value);
            }

            ProductDB Product;

            public Element(ProductDB product) {
                Product = product;
            }

            public Element() {
                Product = admin.NewProduct();
            }
        }
    }
}
