using DBconnectShop.Access;
using DBconnectShop.Table;
using System.Windows.Controls;

namespace ProjektApp.Pages.Profile {
    /// <summary>
    /// Interaction logic for UserAddresses.xaml
    /// </summary>
    public partial class UserAddresses : UserControl {
        UserProfil profil;

        public UserAddresses(UserProfil profil) {
            InitializeComponent();

            this.profil = profil;
            int nr = 1;
            foreach(var tmp in profil.Address) {
                if(!(tmp is null)) {
                    GridData.Items.Add(new AddressData(nr, tmp));
                    nr++;
                }
            }
        }

        class AddressData {
            public string c1 { get; set; }
            public string c2 { get; set; }
            public string c3 { get; set; }
            public string c4 { get; set; }
            public string c5 { get; set; }
            public string c6 { get; set; }

            public AddressData(int nr, Address address) {
                c1 = nr.ToString();
                c2 = address.Country;
                c3 = address.City;
                c4 = address.Street;
                c5 = address.BuildingNumber;
                c6 = address.ZipCode;
            }
        }
    }
}
