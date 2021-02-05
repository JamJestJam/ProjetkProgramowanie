using DBconnectShop;
using DBconnectShop.Table;
using MaterialDesignThemes.Wpf;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjektApp.Pages.Product {
    using Product = DBconnectShop.Table.Product;

    /// <summary>
    /// Interaction logic for ProductInfo.xaml
    /// </summary>
    public partial class ProductInfo : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        SingleProduct singleProduct;
        Product Product => singleProduct.Product;

        public ProductInfo(int ProductID) {
            InitializeComponent();
            singleProduct = new SingleProduct(ProductID);
            Window.TopBar.Content = new TopBar(this);
            Window.LeftPanel.Content = new LeftPanel();

            Reload();
        }

        public void Reload() {
            Window.Loading.IsOpen = true;

            Thread thread = new Thread(ReloadProductInfo) {
                IsBackground = true
            };
            thread.Start();
        }

        private void ReloadProductInfo() {
            singleProduct.Reload();

            Dispatcher.Invoke(() => {
                ReloadContent();
                Window.Loading.IsOpen = false;
            });
        }

        public void ReloadContent() {
            Name.Text = Product.TrueName;
            Price.Text = $"Cena produktu: {Product.ActualPrice.ToString("#,0.00")} zł";

            foreach(var specyfication in Product.Product_Specifications) {
                SetPropertyName(specyfication.Name);
                SetPropertyValue(specyfication.Value);
            }

            foreach(var opinion in Product.Product_Opinions) {
                CreateCommentBox(opinion);
            }

            Rating.Value = singleProduct.AverageRating;

            UserRating.ValueChanged -= Rate;
            UserRating.Value = singleProduct.GetUserRate(Window.login);
            UserRating.ValueChanged += Rate;
        }

        private void SetPropertyName(string name) {
            var propertyName = new Label() {
                Content = name,
                Width = 330,
                Margin = new Thickness(0, 0, 10, 0),
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

        private void AddComment(object o, RoutedEventArgs e) {
            Window.Loading.IsOpen = true;

            Thread thread = new Thread(AddComment) {
                IsBackground = true
            };
            thread.Start();
        }

        private void AddComment() {
            string text = "";
            DBconnectShop.Login login = null;

            Dispatcher.Invoke(() => {
                text = CommentContent.Text;
                login = Window.login;
            });

            try {
                var opinion = singleProduct.AddComment(login, text);

                Dispatcher.Invoke(() => {
                    Window.DialogText.Content = "Dziękujemy za podzielenie się swoją opinią!";
                    Window.Dialog.IsOpen = true;
                    Window.Loading.IsOpen = false;
                    CreateCommentBox(opinion);
                });
            } catch(Exception e) {
                Dispatcher.Invoke(() => {
                    Window.DialogText.Content = e.Message;
                    Window.Dialog.IsOpen = true;
                    Window.Loading.IsOpen = false;
                });
            }
        }

        private void CreateCommentBox(Product_opinion opinion) {
            PackIcon icon = new PackIcon();
            icon.Kind = PackIconKind.User;
            Chip chip = new Chip {
                Margin = new Thickness(0, 5, 0, 0),
                Content = opinion.User.UserName,
                Icon = icon,
                Cursor = Cursors.Arrow
            };

            Label label = new Label {
                Content = opinion.Product_Opinion,
                Background = this.FindResource("MaterialDesignDarkBackground") as Brush
            };

            Comments.Children.Add(chip);
            Comments.Children.Add(label);
        }

        private void Rate(object o, RoutedEventArgs e) {
            Window.Loading.IsOpen = true;
            Thread thread = new Thread(Rate) {
                IsBackground = true
            };
            thread.Start();
        }

        private void Rate() {
            short rate = 0;
            DBconnectShop.Login login = null;

            Dispatcher.Invoke(() => {
                rate = (short)UserRating.Value;
                login = Window.login;
            });

            try {
                singleProduct.AddRate(login, rate);
                Dispatcher.Invoke(() => {
                    Window.DialogText.Content = "Dziękujemy za podzielenie się swoją opinią!";
                    Window.Dialog.IsOpen = true;
                    Window.Loading.IsOpen = false;
                    Rating.Value = singleProduct.AverageRating;
                });
            } catch(Exception e) {
                Dispatcher.Invoke(() => {
                    Window.DialogText.Content = e.Message;
                    Window.Dialog.IsOpen = true;
                    Window.Loading.IsOpen = false;
                });
            }
        }
    }
}
