using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.Employee {
    /// <summary>
    /// Interaction logic for MainEmployee.xaml
    /// </summary>
    public partial class MainEmployee : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        public MainEmployee() {
            InitializeComponent();

            Window.TopBar.Content = new TopBar();
            Window.LeftPanel.Content = new LeftPanel();
        }
    }
}
