using DBconnectShop.Addons;
using DBconnectShop.Table;

namespace DBconnectShop.Access {
    public partial class Admin {
        Login Login;

        public Admin(Login login) {
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
