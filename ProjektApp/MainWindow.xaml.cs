using System.Runtime.InteropServices;
using ProjektApp.Pages;
using System.Windows;
using System.Windows.Input;

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

            
            this.Content.Content = new LoginPage();
            this.LeftPanel.Content = new LoginLeftPage();
            this.TopBarr.Content = new TopBarLoggedOut();
        }

        private void Bar_MouseDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }
    }
}
