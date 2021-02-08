using System.Threading;
using System.Windows;
using System.Windows.Controls;
using LoginDB = DBconnectShop.Access.Login;

namespace ProjektApp.Pages.Login {
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        public Register() {
            InitializeComponent();
        }

        private void RegisterIn(object o, RoutedEventArgs e) {
            Window.Loading.IsOpen = true;

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

                    Window.DialogText.Content = "Rejestracja zakończona pomyślnie";
                    Window.Dialog.IsOpen = true;
                    Window.Loading.IsOpen = false;
                    Content = Login;
                });
            } catch(LoginException e) {
                Dispatcher.Invoke(() => {
                    Window.DialogText.Content = e.Message;
                    Window.Dialog.IsOpen = true;
                    Window.Loading.IsOpen = false;
                });
            }
        }
    }
}
