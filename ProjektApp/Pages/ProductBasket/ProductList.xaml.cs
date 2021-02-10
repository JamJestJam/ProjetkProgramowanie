using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using ProductDB = DBconnectShop.Table.Product;
using ProjektApp;
using System.ComponentModel;

namespace ProjektApp.Pages.ProductBasket {
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;


        public ProductList() {
            InitializeComponent();

            InsertBasket();
        }
        public List<Element> Values { get; set; } = new List<Element>();

        private void InsertBasket() {
            GridData.ItemsSource = Values;

            int lp = 0;
            foreach(var product in Window.basket.ProductList)
                Values.Add(new Element(product, lp++, GridData));
        }
    }

    public class Element {
        static MainWindow Window =>
                Application.Current.MainWindow as MainWindow;
        private KeyValuePair<ProductDB, uint> reference;
        private DataGrid Data { get; }

        public string Lp { get; }
        public string Image { get; }
        public string Name { get; set; }
        public string Price { get; }
        public string Sum {get; set;}

        public string Count {
            get => Window.basket.ProductList[reference.Key].ToString();
            set { 
                Window.basket.SetCount(reference.Key.ID, uint.Parse(value));
                SetSum();
                var tmp = Data.ItemsSource;
                Data.ItemsSource = null;
                Data.ItemsSource = tmp;
            }
        }

        public Element(KeyValuePair<ProductDB, uint> tmp, int lp, DataGrid data) {
            reference = tmp;
            Data = data;

            Lp = lp.ToString();
            Image = "";
            Name = reference.Key.TrueName;
            Price = reference.Key.ActualPrice.ToString();
            SetSum();
        }

        private void SetSum() {
            Sum = (Window.basket.ProductList[reference.Key] * reference.Key.ActualPrice).ToString();
        }
    }
}
