using System.Windows.Controls;
using System.Windows;
using Microsoft.Win32;
using System;

namespace ProjektApp.Pages.Buttons {
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : UserControl {
        public User() {
            InitializeComponent();
        }

        private void Test(object sender, RoutedEventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "cos";
            dlg.DefaultExt = ".png";
            dlg.Filter = "Zdjęcia|*.png;*.jpg;*.jpge";

            Nullable<bool> result = dlg.ShowDialog();
            // Process open file dialog box results
            if(result == true) {
                // Open document
                string filename = dlg.FileName;
            }
        }
    }
}
