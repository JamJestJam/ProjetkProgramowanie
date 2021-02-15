using DBconnectShop.Access;
using Microsoft.Win32;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.Profile {
    /// <summary>
    /// Interaction logic for LeftPanel.xaml
    /// </summary>
    public partial class LeftPanel : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;
        UserProfil profil;

        public LeftPanel(UserProfil profil) {
            InitializeComponent();

            this.profil = profil;
        }

        private void ShowAddresses(object o, RoutedEventArgs e) {
            Window.Content.Content = new UserAddresses(profil);
        }

        private void ShowProfil(object o, RoutedEventArgs e) {
            Window.Content.Content = new UserDataEdit(profil);
        }

        public void SetAvatar(object o, RoutedEventArgs e) {
            Thread thread = new Thread(SetAvatar) {
                IsBackground = true
            };
            thread.Start();
        }

        public void SetAvatar() {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "Zdjęcia|*.png;*.jpg;*.jpge";

            Nullable<bool> result = dlg.ShowDialog();
            if(result == true) {
                string filename = dlg.FileName;

                try {
                    profil.Change_Avatar(filename);

                    Dispatcher.Invoke(() => {
                        Window.DialogText.Content = "Udało się zmienić miniaturkę";
                        Window.Dialog.IsOpen = true;
                    });
                } catch(Exception e) {
                    Dispatcher.Invoke(() => {
                        Window.DialogText.Content = e.Message;
                        Window.Dialog.IsOpen = true;
                    });
                }
            }
            Dispatcher.Invoke(() => {
                Window.Loading.IsOpen = false;
            });
        }

        public void OrderHistory(object o, EventArgs e) {
            Window.Content.Content = new OrderHistory();
        }
    }
}
