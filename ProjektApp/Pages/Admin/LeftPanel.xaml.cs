using System;
using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.Admin {
    /// <summary>
    /// Interaction logic for LeftPanel.xaml
    /// </summary>
    public partial class LeftPanel : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        public LeftPanel() {
            InitializeComponent();
        }

        public void ShowProducts(object o, EventArgs e) {
            Window.Content.Content = new Product.ProductList();
        }

        public void ShowCategories(object o, EventArgs e) {
            Window.Content.Content = new Product.ProductCategories();
        }
    }
}
