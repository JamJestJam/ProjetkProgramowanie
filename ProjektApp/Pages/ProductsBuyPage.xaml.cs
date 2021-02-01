using DBconnectShop;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages {
    /// <summary>
    /// Interaction logic for ProductsBuyPage.xaml
    /// </summary>
    public partial class ProductsBuyPage : UserControl {
        List<UserControl> controls;
        BuyableProducts products;

        int page = 0;
        int perPage = 18;

        public ProductsBuyPage() {
            InitializeComponent();

            controls = new List<UserControl> {
                Product00, Product10, Product20,
                Product01, Product11, Product21,
                Product02, Product12, Product22,
                Product03, Product13, Product23
            };
            products = new BuyableProducts();

            (Application.Current.MainWindow as MainWindow).LeftPanel.Content = new LoggedLeftPageUser(products, this);
            UserPage();
        }

        public void UserPage(int? id = null) {
            var list = products.GetProducts(page, perPage, id);
            foreach(var control in controls)
                control.Content = null;
            for(int i = 0; i < list.Count; i++)
                controls[i].Content = new SingleProduct_ProductsBuyPage(list[i].ProductName, list[i].Products_price);
        }
    }
}
