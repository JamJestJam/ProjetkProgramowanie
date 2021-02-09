using DBconnectShop.Addons;
using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop.Access {
    public class UserProfil {
        private User user;
        int id;

        public string UserName =>
            (user.User_name is null) ?
                "" : user.User_name.Trim();
        public string FirstName =>
            (user.User_Data.User_first_name is null) ?
                "" : user.User_Data.User_first_name.Trim();
        public string SecoundName =>
            (user.User_Data.User_second_name is null) ?
                "" : user.User_Data.User_second_name.Trim();
        public string FamilyName =>
            (user.User_Data.User_family_name is null) ?
                "" : user.User_Data.User_family_name.Trim();

        public List<Address> Address => 
            user.User_Address.Select(a=>a.Address).ToList();

        public UserProfil(Login login) {
            int userID = 0;

            try {
                userID = login.GetUserID;
            } catch {
                throw new AuthorizationException();
            }

            id = userID;
            Reload();
        }

        public void Reload() {
            using var db = new Shop();
            
            var users = db.Users
                .Include(a => a.User_Data)
                .Include(a => a.User_Address)
                    .ThenInclude(b=>b.Address)
                .Where(a => a.User_id == id);

            this.user = users.FirstOrDefault();
        }

        public void Change_Data(string FirstName, string SecoundName, string FamilyName) {
            using var db = new Shop();
            int userID = 0;

            try {
                userID = user.User_id;
            } catch {
                throw new AuthorizationException();
            }

            if(FirstName.Length < 3 || FamilyName.Length < 3)
                throw new AddElementException("Nie wszystkie wymagane pola zostały poprawnie wypełnione.\nSprawdz i spróbuj ponownie.");

            if(user.User_Data is null) {
                var data = new User_data() {
                    User_id = userID,
                    User_first_name = FirstName,
                    User_second_name = SecoundName,
                    User_family_name = FamilyName
                };

                db.Users_Data.Add(data);
            } else {
                var data = db.Users_Data.First(a => a.User_id == userID);

                data.User_first_name = FirstName;
                data.User_second_name = SecoundName;
                data.User_family_name = FamilyName;

                db.Users_Data.Attach(data);
            }

            int code = db.SaveChanges();
            if(code != 1)
                throw new AddElementException("Wystąpił problem z przesłanymi danymi.");
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
