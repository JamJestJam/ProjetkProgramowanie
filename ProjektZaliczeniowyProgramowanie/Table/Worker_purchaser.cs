using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBconnectShop.Table {
    class Worker_purchaser {
        #region Columns ======================================

        [Key]
        [Required]
        public int User_id { get; set; }

        #endregion

        #region Fireign key ==================================

        public User User { get; set; }

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
