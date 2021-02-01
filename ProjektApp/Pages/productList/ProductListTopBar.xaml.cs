using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.productList {
    /// <summary>
    /// Interaction logic for TopBarLogged.xaml
    /// </summary>
    public partial class ProductListTopBar : UserControl {
        public ProductListTopBar() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.Close();
        }
    }
}
