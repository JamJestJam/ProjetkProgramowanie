using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    public class Storage_Product {
        #region Columns ======================================

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Storage_Product_id { get; internal set; }

        [Required]
        public int Product_receipt_id { get; internal set; }

        [Required]
        public int Product_id { get; internal set; }

        [StringLength(100)]
        [Column(TypeName = "nchar")]
        public string Storage_Product_note { get; internal set; }
        #endregion

        #region Fireign key ==================================

        public Product_receipt Product_Receipt { get; }
        public Product Product { get; }
        public IEnumerable<Storage_Product_localization> Storage_Product_Localizations { get; } = new List<Storage_Product_localization>();
        public User_order_product_storage Order { get; }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Storage_Product>().ToTable("Storage_Products");

            modelBuilder.Entity<Storage_Product>()
                .HasOne(a => a.Product_Receipt)
                .WithOne(b => b.Storage_Product)
                .HasForeignKey<Storage_Product>(b => b.Product_receipt_id);

            modelBuilder.Entity<Storage_Product>()
                .HasOne(a => a.Product)
                .WithMany(b => b.Storage_Products)
                .HasForeignKey(b => b.Product_id);
        }
    }
}
