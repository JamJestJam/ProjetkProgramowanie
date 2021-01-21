using Microsoft.EntityFrameworkCore;
using DBconnectShop.Table;

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

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(@"Data Source=(local);Initial Catalog=Shop;Integrated Security=SSPI;");
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
        }
    }
}
