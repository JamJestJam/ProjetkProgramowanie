using DBconnectShop.Addons;
using ProjektApp.Pages.Login;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using LoginDB = DBconnectShop.Access.Login;

namespace ProjektApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public LoginDB login = null;
        public Basket basket = null;

#if DEBUG
        [DllImport("Kernel32")]
        private static extern void AllocConsole();
#endif

        public MainWindow() {
            InitializeComponent();
#if DEBUG
            AllocConsole();
#endif
            Content.Content = new Login();
        }

        private void Bar_MouseDown(object sender, MouseButtonEventArgs e) {
            if(e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void CloseDialog(object o, RoutedEventArgs e) {
            Dialog.IsOpen = false;
        }

        public void LogOut() {
            basket = null;
            login = null;
            Content.Content = new Login();
        }
    }
}
