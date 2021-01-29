using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DBconnectShop {
    /// <summary>
    /// Klasa przechowująca dane użytkownika, po wczesniejszym zalogowaniu
    /// </summary>
    public class Login {
        private readonly User user;

        /// <summary>
        /// Tworzy obiekt klasy
        /// </summary>
        /// <param name="userName">Nazwa użytkownika</param>
        /// <param name="password">Hasło</param>
        public Login(string userName, string password) {
            using var db = new Shop();

            var users = db.Users
                .Where(a => a.User_name == userName)
                .Where(a => a.User_password == password);

#if DEBUG
            Console.WriteLine(users.ToQueryString());
#endif

            if(users.Count() != 1)
                throw new LoginException("Nie poprawna nazwa użytkownika lub hasło");

            this.user = users.First();
        }

        /// <summary>
        /// Tworzy nowego użytkownika
        /// </summary>
        /// <param name="userName">Login użytkownika</param>
        /// <param name="password1">Hasło użytkownika</param>
        /// <param name="password2">Powtórzone hasło użytkownika</param>
        public static void Register(string userName, string password1, string password2) {
            if(password1 != password2)
                throw new LoginException("Hasła nie są identyczne");

            using var db = new Shop();
            var checkLogin = db.Users
                .Where(a => a.User_name == userName);

#if DEBUG
            Console.WriteLine(checkLogin.ToQueryString());
#endif

            if(checkLogin.Count() != 0)
                throw new LoginException("Użytkownik o takim loginie już istnieje");

            var NewUser = new User {
                User_group_id = 1,
                User_name = userName,
                User_password = password1,
            };

            var resoult = db.Users.Add(NewUser);

            int code = db.SaveChanges();
            if(code != 1)
                throw new LoginException("Wystąpił niespodziewany wyjątek podczas tworzenie konta");
        }
    }

    /// <summary>
    /// Wyjątek do logowania
    /// </summary>
    public class LoginException : Exception {
        private string message;

        internal LoginException(string message) {
            this.message = message;
        }

        /// <summary>
        /// Treść błędu
        /// </summary>
        public override string Message => message;
    }
}
