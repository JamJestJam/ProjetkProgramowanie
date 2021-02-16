using DBconnectShop.Access;
using DBconnectShop.Table;
using MaterialDesignThemes.Wpf;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LoginDB = DBconnectShop.Access.Login;

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

            MainImage.Source = Product.FirstImage.ToBitmap();
            Images.Children.Clear();
            foreach(var image in Product.TrueImages()) {
                CreateImage(image.Image.ToImage());
            }

            Comments.Children.Clear();
            foreach(var opinion in Product.Product_Opinions) {
                CreateCommentBox(opinion);
            }

            Rating.Value = singleProduct.AverageRating;

            UserRating.ValueChanged -= Rate;
            UserRating.Value = singleProduct.GetUserRate(Window.login);
            UserRating.ValueChanged += Rate;
        }

        private void CreateImage(Image image) {
            image.Margin = new Thickness(0, 0, 0, 10);
            image.Cursor = Cursors.Hand;
            image.MouseDown += ChangeMain;
            Images.Children.Add(image);
        }

        private void ChangeMain(object o, EventArgs e) {
            var image = (Image)o;
            MainImage.Source = image.Source;
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
            LoginDB login = null;

            Dispatcher.Invoke(() => {
                text = CommentContent.Text;
                login = Window.login;
            });

            try {
                var opinion = singleProduct.AddComment(login, text);

                Dispatcher.Invoke(() => {
                    CreateCommentBox(opinion);
                    Window.DialogText.Content = "Dziękujemy za podzielenie się swoją opinią!";
                    Window.Dialog.IsOpen = true;
                    Window.Loading.IsOpen = false;
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
            Chip chip = new Chip {
                Margin = new Thickness(0, 5, 0, 0),
                Content = opinion.User.UserName,
                Cursor = Cursors.Arrow
            };

            if(opinion.User.User_Data is null || opinion.User.User_Data.User_avatar is null) {
                PackIcon icon = new PackIcon();
                icon.Kind = PackIconKind.User;
                chip.Icon = icon;
            } else {
                var image = opinion.User.User_Data.Image;

                chip.Icon = image.ToImage();
            }

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
            LoginDB login = null;

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

        private void AddBasket(object o, RoutedEventArgs e) {
            var count = (Count.SelectedValue as ComboBoxItem).Content.ToString();
            Window.basket.AddProduct(Product.ID, uint.Parse(count));
        }
    }
}
