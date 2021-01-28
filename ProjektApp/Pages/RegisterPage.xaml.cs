using DBconnectShop;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages {
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : UserControl {
        public RegisterPage() {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e) {
            Hiden.Visibility = Visibility.Visible;

            Thread thread = new Thread(LoginIn);
            thread.IsBackground = true;
            thread.Start();
        }

        private void LoginIn() {
            string userName = "";
            string password1 = "";
            string password2 = "";

            this.Dispatcher.Invoke(() => {
                userName = Login.Text;
                password1 = Password1.Password;
                password2 = Password2.Password;
            });

            try {
                DBconnectShop.Login.Register(userName, password1, password2);

                MessageBox.Show("Rejestrowanie zakończone pomyślnie");

                this.Dispatcher.Invoke(() => {
                    this.Content = new LoginPage();
                });
            } catch(LoginException e) {
                MessageBox.Show(e.Message);

                this.Dispatcher.Invoke(() => {
                    Hiden.Visibility = Visibility.Hidden;
                });
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e) {
            this.Content = new LoginPage();
        }
    }
}
