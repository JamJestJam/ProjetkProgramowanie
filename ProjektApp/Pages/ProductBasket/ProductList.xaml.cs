using System.Windows;
using System.Windows.Controls;

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

        private void InsertBasket() {
            foreach(var product in Window.basket.Product) {
                GridData.Items.Add(new { 
                    Lp=1,
                    Name = product.Key.TrueName,
                    Count = product.Value,
                    Price = product.Key.ActualPrice,
                    Sum = 0,
                    Image = "/Images/no-image.png"
                });
            }
        }
    }
}
