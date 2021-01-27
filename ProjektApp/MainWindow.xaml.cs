using DBconnectShop;
using System;
using System.Runtime.InteropServices;
using System.Windows;

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
            try {
                var login = new Login(this.Login.Text, this.Password.Password);
#if DEBUG
                Console.WriteLine($"{login.GetUserName} {login.GetUserGroup}");
#endif
            } catch(LoginException exception) {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
