using System.Windows.Controls;
using System.Windows.Media;
using DBconnectShop;
using DBconnectShop.Table;
using System.Windows;

namespace ProjektApp.Pages.Product {
    using Product = DBconnectShop.Table.Product;

    /// <summary>
    /// Interaction logic for ProductInfo.xaml
    /// </summary>
    public partial class ProductInfo : UserControl {
        SingleProduct singleProduct;
        Product Product => singleProduct.Product;
        
        public ProductInfo(int ProductID) {
            InitializeComponent();
            singleProduct = new SingleProduct(ProductID);

            SetData();
        }

        public void SetData() {
            Name.Text = Product.TrueName;
            Price.Text = $"Cena produktu: {Product.ActualPrice.ToString("#,0.00")} zł";

            foreach(var specyfication in Product.Product_Specifications) {
                var propertyName = new Label() {
                    Content = specyfication.Name,
                    Width = 340,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    HorizontalContentAlignment = HorizontalAlignment.Right
                };
                var nameSeparator = new Separator();
                var propertyValue = new Label() {
                    Content = specyfication.Value,
                    Width = 340,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    HorizontalContentAlignment = HorizontalAlignment.Left
                };
                var valueSeparator = new Separator();

                PropertyName.Children.Add(propertyName);
                PropertyName.Children.Add(nameSeparator);
                PropertyValue.Children.Add(propertyValue);
                PropertyValue.Children.Add(valueSeparator);
            }
        }
    }
}
