using DBconnectShop.Addons;
using DBconnectShop.Table;

namespace DBconnectShop.Access {
    /// <summary>
    /// Klasa zarządzania możliwościami admina
    /// </summary>
    public partial class AdminControl {
        Login Login;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="login">Autoryzacja</param>
        public AdminControl(Login login) {
            try {
                _ = login.GetUserID;
                if(login.Group != UserGroup.Admin)
                    throw new AuthorizationException();
            } catch {
                throw new AuthorizationException();
            }

            Login = login;
        }
    }
}
