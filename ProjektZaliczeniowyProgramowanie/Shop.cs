using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace DBconnectShop {
    class Shop : DbContext {

        #region Tables =======================================

        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User_group> User_Groups { get; set; }
        public DbSet<User_data> Users_Data { get; set; }
        public DbSet<User_address> User_Addresses { get; set; }
        public DbSet<Worker_seller> Worker_Sellers { get; set; }
        public DbSet<Worker_storekeeper> Worker_Storekeepers { get; set; }
        public DbSet<Worker_purchaser> Worker_Purchasers { get; set; }
        public DbSet<Product_producer> Product_Producers { get; set; }
        public DbSet<Product_categori> Product_Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_specification> Product_Specifications { get; set; }
        public DbSet<Products_price> Products_Prices { get; set; }
        public DbSet<Product_image> Product_Images { get; set; }
        public DbSet<Product_opinion> Product_Opinions { get; set; }
        public DbSet<Product_rating> Product_Ratings { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Product_order> Product_Orders { get; set; }
        public DbSet<Product_receipt> product_Receipts { get; set; }
        public DbSet<Storage_Product> Storage_Products { get; set; }
        public DbSet<Storage_Product_localization> Storage_Product_Localizations { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if(!optionsBuilder.IsConfigured) {
                string path = Path.Combine(Environment.CurrentDirectory, "Shop.mdf");
                optionsBuilder.UseSqlServer($@"Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName={path}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            Address.ModelCreate(modelBuilder);

            User.ModelCreate(modelBuilder);
            User_group.ModelCreate(modelBuilder);
            User_data.ModelCreate(modelBuilder);
            User_address.ModelCreate(modelBuilder);

            Worker_seller.ModelCreate(modelBuilder);
            Worker_storekeeper.ModelCreate(modelBuilder);
            Worker_purchaser.ModelCreate(modelBuilder);

            Product_producer.ModelCreate(modelBuilder);
            Product_categori.ModelCreate(modelBuilder);
            Product.ModelCreate(modelBuilder);
            Product_specification.ModelCreate(modelBuilder);
            Products_price.ModelCreate(modelBuilder);
            Product_image.ModelCreate(modelBuilder);
            Product_opinion.ModelCreate(modelBuilder);
            Product_rating.ModelCreate(modelBuilder);

            Storage.ModelCreate(modelBuilder);
            Product_order.ModelCreate(modelBuilder);
            Product_receipt.ModelCreate(modelBuilder);
            Storage_Product.ModelCreate(modelBuilder);
            Storage_Product_localization.ModelCreate(modelBuilder);
        }
    }
}
