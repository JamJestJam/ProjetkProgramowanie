using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    /// <summary>
    /// komentarze produktu
    /// </summary>
    public class Product_opinion {
        #region Columns ======================================
        /// <summary>
        /// Klucz główny
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_opinion_id { get; internal set; }
        /// <summary>
        /// ID produktu
        /// </summary>
        [Required]
        public int Product_id { get; internal set; }
        /// <summary>
        /// ID uzytkownika
        /// </summary>
        [Required]
        public int User_id { get; internal set; }
        /// <summary>
        /// Tekst komentarza
        /// </summary>
        [Required]
        [StringLength(200)]
        [Column(TypeName = "nchar")]
        public string Product_Opinion { get; internal set; }

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
            modelBuilder.Entity<Product_opinion>().ToTable("Product_opinions");

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
