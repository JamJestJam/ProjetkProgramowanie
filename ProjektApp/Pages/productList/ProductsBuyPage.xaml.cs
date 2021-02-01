using DBconnectShop;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.productList {
    /// <summary>
    /// Interaction logic for ProductsBuyPage.xaml
    /// </summary>
    public partial class ProductsBuyPage : UserControl {
        List<UserControl> controls;
        BuyableProducts products;

        public int Page { get; set; } = 0;
        public int PerPage { get; set; } = 18;
        public int? Category { get; set; } = null;

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

        public void UserPage() {
            var list = products.GetProducts(Page, PerPage, Category);

            foreach(var control in controls)
                control.Content = null;

            for(int i = 0; i < list.Count; i++)
                controls[i].Content = new SingleProduct_ProductsBuyPage(list[i]);
        }
    }
}
