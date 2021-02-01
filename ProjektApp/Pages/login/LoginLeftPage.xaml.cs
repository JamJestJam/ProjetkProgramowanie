using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.login {
    /// <summary>
    /// Interaction logic for LoginLeftPage.xaml
    /// </summary>
    public partial class LoginLeftPage : UserControl {

        private MainWindow Window =>
            (MainWindow)Application.Current.MainWindow;

        public LoginLeftPage() {
            InitializeComponent();
        }

        private void LoginUp_Click(object sender, RoutedEventArgs e) {
            Window.Content.Content = new LoginPage();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e) {
            Window.Content.Content = new RegisterPage();
        }
    }
}
