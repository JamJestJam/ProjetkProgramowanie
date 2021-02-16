using DBconnectShop.Addons;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    /// <summary>
    /// Zdjęcia produktu
    /// </summary>
    public class Product_image {
        #region Columns ======================================
        /// <summary>
        /// Klucz główny
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_image_id { get; internal set; }
        /// <summary>
        /// ID produktu
        /// </summary>
        [Required]
        public int Product_id { get; internal set; }
        /// <summary>
        /// Zdjecie
        /// </summary>
        [Required]
        [Column(TypeName = "varBinary(max)")]
        public byte[] Product_Image { get; internal set; }
        /// <summary>
        /// Aktywność produktu
        /// </summary>
        [Required]
        public bool Product_image_active { get; internal set; }

        #endregion

        #region Fireign key ==================================
        /// <summary>
        /// Produkt ze zdjęcia
        /// </summary>
        public Product Product { get; }

        #endregion

        #region Cuts =========================================
        /// <summary>
        /// Zdjecie
        /// </summary>
        public Image Image =>
            new Image(Product_Image);

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product_image>().ToTable("Product_images");

            modelBuilder.Entity<Product_image>()
                .HasOne(a => a.Product)
                .WithMany(b => b.Product_Images)
                .HasForeignKey(b => b.Product_id);
        }
    }
}
