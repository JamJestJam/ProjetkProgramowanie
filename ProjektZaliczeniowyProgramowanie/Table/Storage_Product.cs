using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBconnectShop.Table {
    class Storage_Product {
        #region Columns ======================================

        [Key]
        [Required]
        public int Storage_Product_id { get; set; }

        [Required]
        public int Product_receipt_id { get; set; }

        [Required]
        public int Product_id { get; set; }

        [StringLength(100)]
        public string Storage_Product_note { get; set; }
        #endregion

        #region Fireign key ==================================

        public Product_receipt Product_Receipt { get; set; }
        public Product Product { get; set; }
        public IEnumerable<Storage_Product_localization> Storage_Product_Localizations { get; set; }

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
