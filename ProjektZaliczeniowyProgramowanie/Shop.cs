using Microsoft.EntityFrameworkCore;
using DBconnectShop.Table;

namespace DBconnectShop {
    class Shop : DbContext {

        #region Tables =======================================

        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User_group> User_Groups { get; set; }
        public DbSet<User_data> Users_Data { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(@"Data Source=(local);Initial Catalog=ShopMarekMichura;Integrated Security=SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            User.ModelCreate(modelBuilder);
            User_group.ModelCreate(modelBuilder);
            User_data.ModelCreate(modelBuilder);
        }
    }
}
