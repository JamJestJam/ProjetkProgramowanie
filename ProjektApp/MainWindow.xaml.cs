using DBconnectShop;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;

namespace ProjektApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

#if DEBUG
        [DllImport("Kernel32")]
        public static extern void AllocConsole();
#endif
        public MainWindow() {
#if DEBUG
            AllocConsole();
#endif
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e) {
            
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
