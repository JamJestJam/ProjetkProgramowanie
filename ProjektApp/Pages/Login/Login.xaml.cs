using DBconnectShop.Addons;
using ProjektApp.Pages.Products;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using LoginDB = DBconnectShop.Access.Login;

namespace ProjektApp.Pages.Login {
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        public Login() {
            InitializeComponent();

            Window.LeftPanel.Content = new LeftPanel();
            Window.TopBar.Content = new TopBar();
        }

        private void LoginIn(object o, RoutedEventArgs e) {
            Window.Loading.IsOpen = true;

            Thread thread = new Thread(LoginIn) {
                IsBackground = true
            };
            thread.Start();
        }

        private void LoginIn() {
            string login = "";
            string password = "";

            Dispatcher.Invoke(() => {
                login = UserName.Text;
                password = Password.Password;
            });

            try {
                var tryLogin = new LoginDB(login, password);
                var basket = new Basket(tryLogin);

                Dispatcher.Invoke(() => {
                    Window.login = tryLogin;
                    Window.basket = basket;
                    Content = new ProductList();
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
