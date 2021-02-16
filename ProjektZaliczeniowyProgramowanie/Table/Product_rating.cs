using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    /// <summary>
    /// Oceny produktu
    /// </summary>
    public class Product_rating {
        #region Columns ======================================
        /// <summary>
        /// Klucz główny
        /// </summary>
        [Required]
        public int Product_id { get; internal set; }
        /// <summary>
        /// ID użytkownika
        /// </summary>
        [Required]
        public int User_id { get; internal set; }
        /// <summary>
        /// Ocena 
        /// </summary>
        [Required]
        [Column(TypeName = "tinyint")]
        public short Product_Rating { get; internal set; }

        #endregion

        #region Fireign key ==================================
        /// <summary>
        /// Produkt
        /// </summary>
        public Product Product { get; }
        /// <summary>
        /// Użytkownik
        /// </summary>
        public User User { get; }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product_rating>().ToTable("Product_ratings");

            modelBuilder.Entity<Product_rating>()
                .HasKey(a => new { a.Product_id, a.User_id });

            modelBuilder.Entity<Product_rating>()
                .HasOne(a => a.Product)
                .WithMany(b => b.Product_Ratings)
                .HasForeignKey(b => b.Product_id);

            modelBuilder.Entity<Product_rating>()
                .HasOne(a => a.User)
                .WithMany(b => b.Product_Ratings)
                .HasForeignKey(b => b.Product_id);
        }
    }
}
