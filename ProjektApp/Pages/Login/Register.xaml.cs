using DBconnectShop;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using LoginDB = DBconnectShop.Login;

namespace ProjektApp.Pages.Login {
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : UserControl {
        public Register() {
            InitializeComponent();
        }

        private void RegisterIn(object o, RoutedEventArgs e) {
            Loading.IsOpen = true;

            Thread thread = new Thread(RegisterIn) {
                IsBackground = true
            };
            thread.Start();
        }

        private void RegisterIn() {
            string userName = "";
            string password1 = "";
            string password2 = "";

            Dispatcher.Invoke(() => {
                userName = UserName.Text;
                password1 = Password1.Password;
                password2 = Password2.Password;
            });

            try {
                LoginDB.Register(userName, password1, password2);

                Dispatcher.Invoke(() => {
                    var Login = new Login();

                    Login.DialogText.Content = "Rejestracja zakończona pomyślnie";
                    Login.Dialog.IsOpen = true;
                    Login.Loading.IsOpen = false;
                    Content = Login;
                });
            } catch(LoginException e) {
                Dispatcher.Invoke(() => {
                    DialogText.Content = e.Message;
                    Dialog.IsOpen = true;
                    Loading.IsOpen = false;
                });
            }
        }

        private void CloseDialog(object o, RoutedEventArgs e) {
            Dialog.IsOpen = false;
        }
    }
}
