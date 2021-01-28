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

            //this.Content = new LoginPage();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Bar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            DragMove();
        }
    }
}
