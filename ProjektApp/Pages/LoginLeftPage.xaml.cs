using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ProjektApp.Pages {
    /// <summary>
    /// Interaction logic for LoginLeftPage.xaml
    /// </summary>
    public partial class LoginLeftPage : UserControl {

        private MainWindow Window =>
            ((MainWindow)Application.Current.MainWindow);

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
