using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBconnectShop.Table {
    public class Worker_purchaser {
        #region Columns ======================================

        [Key]
        [Required]
        public int User_id { get; internal set; }

        #endregion

        #region Fireign key ==================================

        public User User { get; set; }
        public IEnumerable<Product_order> Product_Order { get; }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Worker_purchaser>().ToTable("Worker_purchasers");

            modelBuilder.Entity<Worker_purchaser>()
                .HasOne(a => a.User)
                .WithOne(b => b.Worker_Purchaser)
                .HasForeignKey<Worker_purchaser>(b => b.User_id);
        }
    }
}
