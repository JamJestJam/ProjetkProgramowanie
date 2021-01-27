using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DBconnectShop {
    /// <summary>
    /// Klasa przechowująca dane użytkownika
    /// </summary>
    public class Login {
        private readonly User userData;

        public string GetUserName => userData.User_name;
        public string GetUserGroup => userData.User_Group.User_group_name;

        /// <summary>
        /// Tworzy obiekt Login
        /// </summary>
        /// <param name="login">Nazwa użytkownika</param>
        /// <param name="password">Hasło</param>
        public Login(string login, string password) {
            using var db = new Shop();

            var user = db.Users
                .Include(a => a.User_Group)
                .Where(a => a.User_name == login && a.User_password == password);

#if DEBUG
            Console.WriteLine(user.ToQueryString());
#endif

            if(user.Count() != 1)
                throw new LoginException("Nie poprawna nazwa użytkownika lub hasło");

            userData = user.First();
        }
    }

    /// <summary>
    /// Exception wywoływane w przypadku niepowodzenia logowania
    /// </summary>
    public class LoginException : Exception {
        private string message;

        /// <summary>
        /// Tworzy nowy exception
        /// </summary>
        /// <param name="message">Treść błędu</param>
        public LoginException(string message) {
            this.message = message;
        }

        /// <summary>
        /// Treść błędu logowania
        /// </summary>
        public override string Message => message;
    }
}
