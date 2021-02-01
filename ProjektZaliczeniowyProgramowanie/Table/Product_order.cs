using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    public class Product_order {
        #region Columns ======================================

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_order_id { get; set; }

        [Required]
        public int Storage_id { get; set; }

        [Required]
        public int Worker_purchasers_id { get; set; }

        [Required]
        public int Product_id { get; set; }

        [Required]
        [Column(TypeName = "smallmoney")]
        public decimal Product_order_price { get; set; }

        [Required]
        [Column(TypeName = "smallint")]
        public short Product_order_quantity { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime Product_order_date { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Product_order_estimated_time { get; set; }

        #endregion

        #region Fireign key ==================================

        public Product Product { get; set; }
        public Storage Storage { get; set; }
        public Worker_purchaser Worker { get; set; }
        public Product_receipt Product_Receipt { get; set; }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product_order>().ToTable("Product_orders");

            modelBuilder.Entity<Product_order>()
                .Property(a => a.Product_order_date)
                .HasDefaultValueSql("SYSDATETIME()");

            modelBuilder.Entity<Product_order>()
                .HasOne(a => a.Product)
                .WithMany(b => b.Product_Order)
                .HasForeignKey(b => b.Product_id);

            modelBuilder.Entity<Product_order>()
                .HasOne(a => a.Storage)
                .WithMany(b => b.Product_Order)
                .HasForeignKey(b => b.Storage_id);

            modelBuilder.Entity<Product_order>()
                .HasOne(a => a.Worker)
                .WithMany(b => b.Product_Order)
                .HasForeignKey(b => b.Worker_purchasers_id);
        }
    }
}
