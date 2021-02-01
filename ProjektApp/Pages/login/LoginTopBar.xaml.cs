using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.login {
    /// <summary>
    /// Interaction logic for TopBarLoggedOut.xaml
    /// </summary>
    public partial class LoginTopBar : UserControl {
        public LoginTopBar() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.Close();
        }
    }
}
