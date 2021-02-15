﻿using DBconnectShop.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DBconnectShop.Table;
using ImageAddon = DBconnectShop.Addons.Image;
using Microsoft.Win32;

namespace ProjektApp.Pages.Admin.Product {
    /// <summary>
    /// Interaction logic for ProductImages.xaml
    /// </summary>
    public partial class ProductImages : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;
        AdminProducts admin = new AdminProducts(Window.login);
        public static int ID { get; private set; }
        List<Element> Values { get; set; } = new List<Element>();

        public ProductImages(int id) {
            InitializeComponent();
            ID = id;

            Reload();
        }

        private void Reload() {
            Values = new List<Element>();
            Window.Loading.IsOpen = true;
            Thread thread = new Thread(InitItems) {
                IsBackground = true
            };
            thread.Start();
        }

        private void InitItems() {
            foreach(var product in admin.GetImages(ID)) {
                Dispatcher.Invoke(() => {
                    Values.Add(new Element(product));
                });
            }

            Dispatcher.Invoke(() => {
                Window.Loading.IsOpen = false;
                GridData.ItemsSource = Values;
            });
        }

        public void ChangeImage(object o, EventArgs eventArgs) {
            if(((Button)o).CommandParameter is null)
                return;
            int product = (int)((Button)o).CommandParameter;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "Zdjęcia|*.png;*.jpg;*.jpge";

            Nullable<bool> result = dlg.ShowDialog();
            if(result == true) {
                string filename = dlg.FileName;

                try {
                    admin.ChangeImage(product, filename);
                    Reload();
                } catch(Exception e) {
                    Dispatcher.Invoke(() => {
                        Window.DialogText.Content = e.Message;
                        Window.Dialog.IsOpen = true;
                    });
                }
            }
        }

        class Element {
            static MainWindow Window =>
                            Application.Current.MainWindow as MainWindow;
            AdminProducts admin = new AdminProducts(Window.login);

            public int ID =>
                Product.Product_image_id;

            public bool Active {
                get => Product.Product_image_active;
                set => admin.ChangeActive(Product, value);
            }
            Product_image Product { get; }
            public BitmapImage Image =>
                Product.Image.ToBitmap();

            public Element(Product_image image) {
                Product = image;
            }

            public Element() {
                Product = admin.NewImage(ProductImages.ID);
            }
        }
    }
}
