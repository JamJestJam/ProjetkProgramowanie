using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.Admin {
    /// <summary>
    /// Interaction logic for MainAdmin.xaml
    /// </summary>
    public partial class MainAdmin : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        public MainAdmin() {
            InitializeComponent();

            Window.TopBar.Content = new TopBar();
            Window.LeftPanel.Content = new LeftPanel();
        }
    }
}
