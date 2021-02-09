using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DBconnectShop.Access;
using Microsoft.Win32;

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

        public void SetAvatar(object o, RoutedEventArgs e) {
            Thread thread = new Thread(SetAvatar) {
                IsBackground = true
            };
            thread.Start();
        }

        public void SetAvatar() {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "cos";
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
    }
}
