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
        internal LoginDB login = null;

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
            DragMove();
        }

        private void CloseDialog(object o, RoutedEventArgs e) {
            Dialog.IsOpen = false;
        }
    }
}
