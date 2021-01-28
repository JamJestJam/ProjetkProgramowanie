using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    class Product_image {
        #region Columns ======================================

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_image_id { get; set; }

        [Required]
        public int Product_id { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nchar")]
        public string Product_Image { get; set; }

        #endregion

        #region Fireign key ==================================

        public Product Product { get; set; }

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
