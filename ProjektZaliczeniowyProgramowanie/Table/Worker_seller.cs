using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBconnectShop.Table {
    public class Worker_seller {
        #region Columns ======================================

        [Key]
        [Required]
        public int User_id { get; internal set; }

        #endregion

        #region Fireign key ==================================

        public User User { get; }
        public IEnumerable<User_order_receipt> Order_Receipts { get; } = new List<User_order_receipt>();

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Worker_seller>().ToTable("Worker_sellers");

            modelBuilder.Entity<Worker_seller>()
                .HasOne(a => a.User)
                .WithOne(b => b.Worker_Seller)
                .HasForeignKey<Worker_seller>(b => b.User_id);

            modelBuilder.Entity<Worker_seller>()
                .HasData(new Worker_seller() {
                    User_id = 1,
                });
        }
    }
}
