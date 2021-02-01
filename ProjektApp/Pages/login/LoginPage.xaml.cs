using DBconnectShop;
using ProjektApp.Pages.productList;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.login {
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

                this.Dispatcher.Invoke(() => {
                    Hidden.IsOpen = false;
                    this.Content = new ProductsBuyPage();
                    (Application.Current.MainWindow as MainWindow).TopBarr.Content = new TopBarLogged();
                });
            } catch(LoginException e) {
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
