using DBconnectShop.Access;
using System;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using LoginDB = DBconnectShop.Access.Login;

namespace ProjektApp.Pages.Profile {
    /// <summary>
    /// Interaction logic for UserDataEdit.xaml
    /// </summary>
    public partial class UserDataEdit : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;
        UserProfil profil;

        public UserDataEdit() {
            InitializeComponent();
            profil = new UserProfil(Window.login);

            Window.TopBar.Content = new TopBar();
            Window.LeftPanel.Content = new LeftPanel(profil);

            UseData();
        }

        public void UseData() {
            Window.Loading.IsOpen = true;
            Thread thread = new Thread(DownloadData) {
                IsBackground = true
            };
            thread.Start();
        }

        public void DownloadData() {
            LoginDB login = null;
            Dispatcher.Invoke(() => {
                login = Window.login;
            });
            profil.Reload();

            Dispatcher.Invoke(() => {
                Insert();
                Window.Loading.IsOpen = false;
            });
        }

        private void Save(object o, RoutedEventArgs e) {
            Window.Loading.IsOpen = true;
            Thread thread = new Thread(Save) {
                IsBackground = true
            };
            thread.Start();
        }

        private void Save() {
            string fName = "", sName = "", Fname = "";
            Dispatcher.Invoke(() => {
                fName = FirstName.Text;
                sName = SecoundName.Text;
                Fname = FamilyName.Text;
            });

            try {
                profil.Change_Data(fName, sName, Fname);

                UseData();
            } catch(Exception e) {
                Dispatcher.Invoke(() => {
                    Window.DialogText.Content = e.Message;
                    Window.Dialog.IsOpen = true;
                });
            }

            Dispatcher.Invoke(() => {
                Window.Loading.IsOpen = false;
            });
        }

        private void Insert() {
            UserName.Text = profil.UserName;
            FirstName.Text = profil.FirstName;
            SecoundName.Text = profil.SecoundName;
            FamilyName.Text = profil.FamilyName;
        }
    }
}
