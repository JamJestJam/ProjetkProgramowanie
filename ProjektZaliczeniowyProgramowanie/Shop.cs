using Microsoft.EntityFrameworkCore;
using ProjektZaliczeniowyProgramowanie.Table;

namespace ProjektZaliczeniowyProgramowanie {
    class ShopMarekMichura : DbContext {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User_group> User_Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(@"Data Source=(local);Initial Catalog=ShopMarekMichura;Integrated Security=SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
