using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    public class Product_receipt {
        #region Columns ======================================

        [Key]
        [Required]
        public int Product_order_id { get; internal set; }

        [Required]
        public int Worker_storekeeper_id { get; internal set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime Product_receipt_date { get; internal set; }

        #endregion

        #region Fireign key ==================================

        public Product_order Product_Order { get; }
        public Worker_storekeeper Worker { get; }
        public Storage_Product Storage_Product { get; }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product_receipt>().ToTable("Product_receipts");

            modelBuilder.Entity<Product_receipt>()
                .Property(a => a.Product_receipt_date)
                .HasDefaultValueSql("SYSDATETIME()");

            modelBuilder.Entity<Product_receipt>()
                .HasOne(a => a.Worker)
                .WithMany(b => b.Product_Receipts)
                .HasForeignKey(b => b.Worker_storekeeper_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product_receipt>()
                .HasOne(a => a.Product_Order)
                .WithOne(b => b.Product_Receipt)
                .HasForeignKey<Product_receipt>(b => b.Product_order_id);
        }
    }
}
