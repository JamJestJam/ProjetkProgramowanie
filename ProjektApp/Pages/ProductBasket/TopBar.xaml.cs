using ProjektApp.Pages.Buttons;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.ProductBasket {
    /// <summary>
    /// Interaction logic for TopBar.xaml
    /// </summary>
    public partial class TopBar : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;
        Type back;
        
        /// <summary>
        /// Menu nawigacyjne dla listy produktów w koszyku
        /// </summary>
        /// <param name="type">Klasa do której ma wracać</param>
        public TopBar(Type type) {
            InitializeComponent();

            this.back = type;
            UserBtn.Content = new User();
            Exit.Content = new Exit();
            Basket.Content = new Basket();
        }

        private void GoBack(object sender, RoutedEventArgs e) {
            Window.Content.Content = Activator.CreateInstance(back);
        }
    }
}
