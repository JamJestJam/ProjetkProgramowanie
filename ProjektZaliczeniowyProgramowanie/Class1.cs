using System;
using System.Linq;
using ProjektZaliczeniowyProgramowanie.Table;

namespace ProjektZaliczeniowyProgramowanie {
    public class Class {
        public static void Main() {
            using var db = new ShopMarekMichura();

            Console.WriteLine("");
            
            IQueryable<User> users = db.Users;

            //Console.WriteLine(db.Database.CanConnect());

            foreach(var u in users) {
                Console.WriteLine($"{u.User_name}, {u.User_password}");
            }
        }
    }
}
