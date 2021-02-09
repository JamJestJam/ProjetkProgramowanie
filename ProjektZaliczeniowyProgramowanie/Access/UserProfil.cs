using DBconnectShop.Addons;
using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DBconnectShop.Access {
    public class UserProfil {
        private User user;

        public UserProfil(Login login) {
            using var db = new Shop();
            int userID = 0;

            try {
                userID = login.GetUserID;
            } catch {
                throw new AuthorizationException();
            }

            var users = db.Users
                .Include(a => a.User_Data)
                .Include(a => a.User_Address)
                .Where(a => a.User_id == userID);

            this.user = users.FirstOrDefault();
        }

        public void Change_Avatar(string file) {
            using var db = new Shop();

            var image = new Image(file);
            var User_Data = user.User_Data;
            if(User_Data is null) 
                throw new AddElementException("Aby dodać miniaturkę musisz posiadać wypełniony profil użytkownika.");

            db.Users_Data.Attach(User_Data);            

            User_Data.User_avatar = image.BlobImage;

            int code = db.SaveChanges();
            if(code != 1)
                throw new AddElementException("Wystąpił problem z przesłanym avatarem.");
        }
    }
}
