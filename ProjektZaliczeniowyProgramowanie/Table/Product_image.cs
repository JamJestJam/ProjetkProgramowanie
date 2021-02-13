using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    public class Product_image {
        #region Columns ======================================

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_image_id { get; internal set; }

        [Required]
        public int Product_id { get; internal set; }

        [Required]
        [MaxLength(8000)]
        [Column(TypeName = "varBinary")]
        public byte[] Product_Image { get; internal set; }


        #endregion

        #region Fireign key ==================================

        public Product Product { get; }

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
