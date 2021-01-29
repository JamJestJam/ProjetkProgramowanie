using DBconnectShop;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes;
using MaterialDesignThemes.Wpf;

namespace ProjektApp.Pages {
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl {
        public LoginPage() {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e) {
            this.Content = new RegisterPage();
        }

        private void Submit_Click(object sender, RoutedEventArgs e) {
            Hidden.IsOpen = true;

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
            } catch (LoginException e) {
                this.Dispatcher.Invoke(() => {
                    DialogText.Content = e.Message;
                    Dialog.IsOpen = true;
                    Hidden.IsOpen = false;
                });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Dialog.IsOpen = false;
        }
    }
}
