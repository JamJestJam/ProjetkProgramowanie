using DBconnectShop.Access;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.Buttons {
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        public User() {
            InitializeComponent();
        }

        private void Test(object sender, RoutedEventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "cos";
            dlg.DefaultExt = ".png";
            dlg.Filter = "Zdjęcia|*.png;*.jpg;*.jpge";

            Nullable<bool> result = dlg.ShowDialog();
            if(result == true) {
                string filename = dlg.FileName;

                var profil = new UserProfil(Window.login);
                profil.Change_Avatar(filename);
            }
        }

        private void LogOut(object o, RoutedEventArgs e) {
            Window.LogOut();
        }
    }
}
