using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBconnectShop.Table {
    class Product_receipt {
        #region Columns ======================================

        [Key]
        [Required]
        public int Product_order_id { get; set; }

        [Required]
        public int Worker_storekeeper_id { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime Product_receipt_date { get; set; }

        #endregion

        #region Fireign key ==================================

        public Product_order Product_Order { get; set; }
        public Worker_storekeeper Worker { get; set; }
        public Storage_Product Storage_Product { get; set; }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product_receipt>().ToTable("Product_receipts");

            modelBuilder.Entity<Product_receipt>()
                .HasOne(a => a.Worker)
                .WithMany(b => b.Product_Receipts)
                .HasForeignKey(b => b.Worker_storekeeper_id);

            modelBuilder.Entity<Product_receipt>()
                .HasOne(a => a.Product_Order)
                .WithOne(b => b.Product_Receipt)
                .HasForeignKey<Product_receipt>(b => b.Product_order_id);
        }
    }
}
