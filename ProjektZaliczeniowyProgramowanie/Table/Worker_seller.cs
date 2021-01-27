using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBconnectShop.Table {
    class Worker_seller {
        #region Columns ======================================

        [Key]
        [Required]
        public int User_id { get; set; }

        #endregion

        #region Fireign key ==================================

        public User User { get; set; }
        public IEnumerable<User_order_receipt> Order_Receipts { get; set; } = new List<User_order_receipt>();

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Worker_seller>().ToTable("Worker_sellers");

            modelBuilder.Entity<Worker_seller>()
                .HasOne(a => a.User)
                .WithOne(b => b.Worker_Seller)
                .HasForeignKey<Worker_seller>(b => b.User_id);
        }
    }
}
