using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.Login {
    /// <summary>
    /// Interaction logic for TopBar.xaml
    /// </summary>
    public partial class TopBar : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        public TopBar() {
            InitializeComponent();
        }

        private void Close(object o, RoutedEventArgs e) {
            Window.Close();
        }
    }
}
