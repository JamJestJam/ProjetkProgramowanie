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
                SetPropertyName(specyfication.Name);
                SetPropertyValue(specyfication.Value);
            }
        }

        private void SetPropertyName(string name) {
            var propertyName = new Label() {
                Content = name,
                Width = 330,
                Margin = new Thickness(0,0,10,0),
                HorizontalAlignment = HorizontalAlignment.Right,
                HorizontalContentAlignment = HorizontalAlignment.Right
            };
            var nameSeparator = new Separator();

            PropertyName.Children.Add(propertyName);
            PropertyName.Children.Add(nameSeparator);
        }

        private void SetPropertyValue(string value) {
            var propertyValue = new Label() {
                Content = value,
                Width = 330,
                Margin = new Thickness(10, 0, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                HorizontalContentAlignment = HorizontalAlignment.Left
            };
            var valueSeparator = new Separator();

            PropertyValue.Children.Add(propertyValue);
            PropertyValue.Children.Add(valueSeparator);
        }
    }
}
