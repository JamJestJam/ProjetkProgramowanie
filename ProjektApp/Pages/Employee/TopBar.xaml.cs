using ProjektApp.Pages.Buttons;
using ProjektApp.Pages.Products;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.Employee {
    /// <summary>
    /// Interaction logic for TopBar.xaml
    /// </summary>
    public partial class TopBar : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        public TopBar() {
            InitializeComponent();

            UserBtn.Content = new User();
            Exit.Content = new Exit();
            Basket.Content = new Basket();
        }

        public void GoBack(object o, EventArgs e) {
            Window.Content.Content = new ProductList();
        }
    }
}
