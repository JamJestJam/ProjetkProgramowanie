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

        /// <summary>
        /// Tworzy widok dla lewego panelu w panelu admina
        /// </summary>
        public LeftPanel() {
            InitializeComponent();
        }

        private void ShowProducts(object o, EventArgs e) {
            Window.Content.Content = new Product.ProductList();
        }

        private void ShowCategories(object o, EventArgs e) {
            Window.Content.Content = new Product.ProductCategories();
        }
    }
}
