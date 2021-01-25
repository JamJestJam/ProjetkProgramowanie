using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop {
    public class Class {
        public static void Main() {
            //AllUsers();
            //Group();
            //Addresses();
            //Workers();
            Products();
            //Categores();
        }

        static void AllUsers() {
            using var db = new Shop();

            IQueryable<User> users = db.Users
                .Include(a => a.User_Group)
                .Include(a => a.User_Data)
                .Include(a => a.User_Address).ThenInclude(b => b.Address)
                .Include(a => a.Worker_Seller)
                .Include(a => a.Worker_Storekeeper);

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

        static void Workers() {
            using var db = new Shop();

            IQueryable<Worker_seller> sellers = db.Worker_Sellers
                .Include(a => a.User).ThenInclude(b => b.User_Address).ThenInclude(c => c.Address)
                .Include(a => a.User).ThenInclude(b => b.User_Data);

            IQueryable<Worker_storekeeper> storekeepers = db.Worker_Storekeepers
                .Include(a => a.User).ThenInclude(b => b.User_Address).ThenInclude(c => c.Address)
                .Include(a => a.User).ThenInclude(b => b.User_Data);

            IQueryable<Worker_purchaser> purchasers = db.Worker_Purchasers
                .Include(a => a.User).ThenInclude(b => b.User_Address).ThenInclude(c => c.Address)
                .Include(a => a.User).ThenInclude(b => b.User_Data);

            Console.WriteLine("Sellers: ");
            foreach (var a in sellers) {
                Console.WriteLine($"\t{a.User.User_name}");
            }

            Console.WriteLine("Storekeepers: ");
            foreach (var a in storekeepers) {
                Console.WriteLine($"\t{a.User.User_name}");
            }

            Console.WriteLine("purchasers: ");
            foreach (var a in purchasers) {
                Console.WriteLine($"\t{a.User.User_name}");
            }
        }

        static void Products() {
            using var db = new Shop();

            IQueryable<Product_producer> product_Producers = db.Product_Producers
                .Include(a => a.Products).ThenInclude(b => b.Product_Categori)
                .Include(a => a.Products).ThenInclude(b => b.Product_Specifications)
                .Include(a => a.Products).ThenInclude(b => b.Products_Prices);

            Console.WriteLine("Products: ");
            foreach (var p in product_Producers) {
                Console.WriteLine($"{p.Product_producer_name}");
            }
        }

        static void Categores() {
            using var db = new Shop();

            IQueryable<Product_categori> product_Categories = db.Product_Categories
                .Include(a => a.Children);

            Console.WriteLine("Product categories: ");
            foreach (var p in product_Categories) {
                Console.WriteLine($"{p.Product_category_name}");
            }
        }
    }
}
