using DBconnectShop.Access;
using DBconnectShop.Table;
using Microsoft.Win32;
using ProjektApp.Pages.Admin;
using ProjektApp.Pages.Employee;
using ProjektApp.Pages.Profile;
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

            switch(Window.login.Group) {
                case UserGroup.User:
                    WorkerBt.Visibility = Visibility.Collapsed;
                    AdminBt.Visibility = Visibility.Collapsed;
                    break;
            }
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

        private void Profile(object o, RoutedEventArgs e) {
            Window.Content.Content = new UserDataEdit();
        }

        private void Admin(object o, RoutedEventArgs e) {
            Window.Content.Content = new MainAdmin();
        }

        private void Worker(object o, RoutedEventArgs e) {
            Window.Content.Content = new MainEmployee();
        }

        private void LogOut(object o, RoutedEventArgs e) {
            Window.LogOut();
        }
    }
}
