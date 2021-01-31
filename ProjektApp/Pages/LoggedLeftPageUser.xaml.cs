using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using DBconnectShop;

namespace ProjektApp.Pages {
    /// <summary>
    /// Interaction logic for LoggedLeftPageUser.xaml
    /// </summary>
    public partial class LoggedLeftPageUser : UserControl {
        BuyableProducts Products;

        public LoggedLeftPageUser(BuyableProducts products) {
            InitializeComponent();
            Products = products;
        }
    }
}
