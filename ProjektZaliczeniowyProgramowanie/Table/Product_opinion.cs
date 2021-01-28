using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    class Product_opinion {
        #region Columns ======================================

        [Required]
        public int Product_id { get; set; }

        [Required]
        public int User_id { get; set; }

        [Required]
        [StringLength(200)]
        [Column(TypeName = "nchar")]
        public string Product_Opinion { get; set; }

        #endregion

        #region Fireign key ==================================

        public Product Product { get; set; }
        public User User { get; set; }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product_opinion>().ToTable("Product_opinions");

            modelBuilder.Entity<Product_opinion>()
                .HasKey(a => new { a.Product_id, a.User_id });

            modelBuilder.Entity<Product_opinion>()
                .HasOne(a => a.Product)
                .WithMany(b => b.Product_Opinions)
                .HasForeignKey(b => b.Product_id);

            modelBuilder.Entity<Product_opinion>()
                .HasOne(a => a.User)
                .WithMany(b => b.Product_Opinions)
                .HasForeignKey(b => b.User_id);
        }
    }
}
