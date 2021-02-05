using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.Buttons {
    /// <summary>
    /// Interaction logic for Exit.xaml
    /// </summary>
    public partial class Exit : UserControl {
        public Exit() {
            InitializeComponent();
        }

        private void Close(object o, RoutedEventArgs e) {
            Application.Current.MainWindow.Close();
        }
    }
}
