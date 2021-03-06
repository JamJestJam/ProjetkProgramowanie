﻿using DBconnectShop.Addons;
using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DBconnectShop.Access {
    /// <summary>
    /// Klasa przechowująca dane użytkownika, po wczesniejszym zalogowaniu
    /// </summary>
    public class Login {
        private readonly User user;
        /// <summary>
        /// Pobiera dane użytkownika
        /// </summary>
        public int GetUserID => user.User_id;
        /// <summary>
        /// Grupa do której należy uzytkownik
        /// </summary>
        public UserGroup Group => user.User_Group.Group;

        /// <summary>
        /// Tworzy obiekt klasy
        /// </summary>
        /// <param name="userName">Nazwa użytkownika</param>
        /// <param name="password">Hasło</param>
        public Login(string userName, string password) {
            using var db = new Shop();

            var users = db.Users
                .Include(a => a.User_Group)
                .Where(a => a.User_name.Equals(userName))
                .Where(a => a.User_password.Equals(password));

#if DEBUG
            Console.WriteLine(users.ToQueryString());
#endif

            if(users.Count() != 1)
                throw new LoginException("Nie poprawna nazwa użytkownika lub hasło");

            var user = users.First();

            if(!user.User_active)
                throw new LoginException("Konto na które próbujesz się zalogować zostało zablokowane");

            this.user = user;
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

            if(checkLogin.Any())
                throw new LoginException("Użytkownik o takim loginie już istnieje");

            var NewUser = new User {
                User_group_id = 1,
                User_name = userName,
                User_password = password1,
            };

            db.Users.Add(NewUser);

            int code = db.SaveChanges();
            if(code != 1)
                throw new LoginException("Wystąpił niespodziewany wyjątek podczas tworzenie konta");
        }

    }
}
