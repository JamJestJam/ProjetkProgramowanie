using System;
using System.Linq;
using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;

namespace DBconnectShop {
    public class Class {
        public static void Main() {
            AllUsers();
            Group();
            Addresses();
        }

        static void AllUsers() {
            using var db = new Shop();

            IQueryable<User> users = db.Users
                .Include(a => a.User_Group)
                .Include(a => a.User_Data);

            foreach (var u in users) {
                Console.WriteLine($"{u.User_name}, {u.User_password}");
            }
        }

        static void Group() {
            using var db = new Shop();

            IQueryable<User_group> groups = db.User_Groups
                .Include(a => a.Users)
                .ThenInclude(b => b.User_Data);

            foreach (var g in groups) {
                Console.WriteLine($"{g.User_group_name}: ");
                foreach (var u in g.Users) {
                    Console.WriteLine($"\t{u.User_name}");
                }
            }
        }

        static void Addresses() {
            using var db = new Shop();

            IQueryable<Address> addresses = db.Addresses
                .Include(a => a.User_Addresses);

            foreach (var a in addresses) {
                Console.WriteLine($"{a.Address_city}: ");
            }
        }
    }
}
