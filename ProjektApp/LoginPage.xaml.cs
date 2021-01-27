using DBconnectShop;
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

namespace ProjektApp {
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl {
        public LoginPage() {
            InitializeComponent();

            Application.Current.MainWindow.Width = 350;
            Application.Current.MainWindow.Height = 325;
        }

        private void Register_Click(object sender, RoutedEventArgs e) {
            this.Content = new RegisterPage();
        }

        private void Submit_Click(object sender, RoutedEventArgs e) {
            Hiden.Visibility = Visibility.Visible;

            var login = new Login(Login.Text, Password.Password);
            login.Sprawdzono += CheckLogin;

            Thread thread = new Thread(login.TryLogin);
            thread.IsBackground = true;
            thread.Start();
        }

        private void CheckLogin(object sender, EventLoginDone e) {
            this.Dispatcher.Invoke(() => {
                Hiden.Visibility = Visibility.Hidden;
            });

            if(e.Success) {
#if DEBUG
                Console.WriteLine("Udało się");
#endif

            } else {
#if DEBUG
                Console.WriteLine("Nie udało się");
#endif
            }
        }
    }
}
