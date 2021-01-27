using DBconnectShop;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

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

            Thread thread = new Thread(LoginIn);
            thread.IsBackground = true;
            thread.Start();
        }

        private void LoginIn() {
            string userName = "";
            string password = "";

            this.Dispatcher.Invoke(() => {
                userName = Login.Text;
                password = Password.Password;
            });

            try {
                var Login = new Login(userName, password);
#if DEBUG
                Console.WriteLine("Login in");
#endif
            } catch {
#if DEBUG
                Console.WriteLine("Crash");
#endif
                this.Dispatcher.Invoke(() => {
                    Hiden.Visibility = Visibility.Hidden;
                });
            }
        }
    }
}
