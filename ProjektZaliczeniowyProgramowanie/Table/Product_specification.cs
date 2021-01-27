using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    class Product_specification {
        #region Columns ======================================

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_specification_id { get; set; }

        [Required]
        public int Product_id { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName ="nchar")]
        public string Product_specification_name { get; set; }

        [Required]
        [StringLength(25)]
        [Column(TypeName ="nchar")]
        public string Product_specification_value { get; set; }

        #endregion

        #region Fireign key ==================================

        public Product Product { get; set; }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product_specification>().ToTable("Product_specifications");

            modelBuilder.Entity<Product_specification>()
                .HasOne(a => a.Product)
                .WithMany(b => b.Product_Specifications)
                .HasForeignKey(b => b.Product_id);
        }
    }
}
