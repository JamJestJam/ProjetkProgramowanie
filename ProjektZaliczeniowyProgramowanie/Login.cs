using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DBconnectShop {
    /// <summary>
    /// Klasa przechowująca dane użytkownika
    /// </summary>
    public class Login {
        private User userData;
        private readonly string login;
        private readonly string password;

        /// <summary>
        /// Pobiera nazwe uzytkownika
        /// </summary>
        public string GetUserName => userData.User_name;
        /// <summary>
        /// Pobiera nazwe grupy do której należy użytkownik
        /// </summary>
        public string GetUserGroup => userData.User_Group.User_group_name;

        public delegate void LoginDone(object sender, EventLoginDone e);
        public event LoginDone Sprawdzono;

        /// <summary>
        /// Tworzy obiekt Login
        /// </summary>
        /// <param name="login">Nazwa użytkownika</param>
        /// <param name="password">Hasło</param>
        public Login(string login, string password) {
            this.login = login;
            this.password = password;
        }

        public void TryLogin() {
            if(userData != null)
                throw new LoggedInException();

            using var db = new Shop();

            var user = db.Users
                .Include(a => a.User_Group)
                .Where(a => a.User_name == login && a.User_password == password);
#if DEBUG
            Console.WriteLine(user.ToQueryString());
#endif
            if(user.Count() != 1)
                SendEvent(new EventLoginDone(login, false));
            else {
                userData = user.First();
                SendEvent(new EventLoginDone(login, true));
            }
        }

        private void SendEvent(EventLoginDone e) {
            if(Sprawdzono != null) {
                Sprawdzono(this, e);
            }
        }
    }

    /// <summary>
    /// Wyjątek spowodowany próbą zalogowania na po raz kolejny
    /// </summary>
    public class LoggedInException:Exception {
        /// <summary>
        /// Treść błędu
        /// </summary>
        public override string Message => "Already logged in";
    }

    /// <summary>
    /// Klasa zawierające parametry zdarzenia.
    /// </summary>
    public class EventLoginDone : EventArgs {
        /// <summary>
        /// Nazwa użytkonika który próbował się zalogować
        /// </summary>
        public string User_name { get; }
        /// <summary>
        /// Informacja czy próba logowania powiodła się
        /// <para>True - oznacza że logowanie się powiodło</para>
        /// <para>False - oznacza że logowanie się nie powiodło</para>
        /// </summary>
        public bool Success { get; }

        internal EventLoginDone(string user, bool success) {
            this.User_name = user;
            this.Success = success;
        }
    }
}
