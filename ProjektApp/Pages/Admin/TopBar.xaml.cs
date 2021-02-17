using ProjektApp.Pages.Buttons;
using ProjektApp.Pages.Products;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.Admin {
    /// <summary>
    /// Interaction logic for TopBar.xaml
    /// </summary>
    public partial class TopBar : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        /// <summary>
        /// Tworzy pasek nawigacyjny dla widoku admina
        /// </summary>
        public TopBar() {
            InitializeComponent();

            UserBtn.Content = new User();
            Exit.Content = new Exit();
            Basket.Content = new Basket();
        }

        private void GoBack(object o, EventArgs e) {
            Window.Content.Content = new ProductList();
        }
    }
}
