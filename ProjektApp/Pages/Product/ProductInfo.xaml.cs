﻿using DBconnectShop;
using DBconnectShop.Table;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;

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

            SetData();
        }

        public void SetData() {
            Name.Text = Product.TrueName;
            Price.Text = $"Cena produktu: {Product.ActualPrice.ToString("#,0.00")} zł";

            foreach(var specyfication in Product.Product_Specifications) {
                SetPropertyName(specyfication.Name);
                SetPropertyValue(specyfication.Value);
            }

            foreach(var opinion in Product.Product_Opinions) {
                CreateCommentBox(opinion);
            }
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
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("/Images/no-image.png", UriKind.Relative);
            bitmap.EndInit();
            Image img = new Image {
                Source = bitmap
            };
            Chip chip = new Chip {
                Content = opinion.User.UserName,
                Icon = img,
                Cursor = Cursors.Arrow
            };

            Label label = new Label {
                Content = opinion.Product_Opinion,
                Background = this.FindResource("MaterialDesignDarkBackground") as Brush
            };

            Comments.Children.Add(chip);
            Comments.Children.Add(label);
        }
    }
}
