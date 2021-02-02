using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.Login {
    /// <summary>
    /// Interaction logic for LeftPanel.xaml
    /// </summary>
    public partial class LeftPanel : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        public LeftPanel() {
            InitializeComponent();
        }

        private void ToLoginPanel(object s, RoutedEventArgs e) {
            Window.Content.Content = new Login();
        }

        private void ToRegisterPanel(object s, RoutedEventArgs e) {
            Window.Content.Content = new Register();
        }
    }
}
