using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    class User_order_receipt {
        #region Columns ======================================

        [Key]
        [Required]
        public int User_order_id { get; set; }

        [Required]
        public int Worker_seller_id { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime User_order_recipt_date { get; set; }

        #endregion

        #region Fireign key ==================================

        public User_order Order { get; set; }
        public Worker_seller Worker { get; set; }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User_order_receipt>().ToTable("User_order_receipts");

            modelBuilder.Entity<User_order_receipt>()
                .HasOne(a => a.Order)
                .WithOne(b => b.Order_Receipt)
                .HasForeignKey<User_order_receipt>(b => b.User_order_id);

            modelBuilder.Entity<User_order_receipt>()
                .HasOne(a => a.Worker)
                .WithMany(b => b.Order_Receipts)
                .HasForeignKey(b => b.Worker_seller_id);
        }
    }
}
