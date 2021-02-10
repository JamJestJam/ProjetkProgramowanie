using System.Windows;
using System.Windows.Controls;
using DBconnectShop.Addons;
using BasketDB = DBconnectShop.Addons.Basket;

namespace ProjektApp.Pages.Buttons {
    /// <summary>
    /// Interaction logic for Basket.xaml
    /// </summary>
    public partial class Basket : UserControl {
        static MainWindow Window =>
            Application.Current.MainWindow as MainWindow;

        public Basket() {
            InitializeComponent();

            ShowBasket(Window.basket.Count);
            Window.basket.OnChange += basketChange;
        }

        private void basketChange(BasketDB basket, BasketChangeEventArgs e) {
            ShowBasket(Window.basket.Count);
        }

        private void ShowBasket(long count) {
            if(count == 0)
                Count.Badge = "";
            else if(count > 99)
                Count.Badge = "99+";
            else
                Count.Badge = count;
        }
    }
}
