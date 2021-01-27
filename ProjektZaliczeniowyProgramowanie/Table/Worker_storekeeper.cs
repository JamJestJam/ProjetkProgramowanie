﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBconnectShop.Table {
    class Worker_storekeeper {
        #region Columns ======================================

        [Key]
        [Required]
        public int User_id { get; set; }

        #endregion

        #region Fireign key ==================================

        public User User { get; set; }
        public IEnumerable<Product_receipt> Product_Receipts { get; set; }
        public IEnumerable<Storage_Product_localization> Storage_Product_Localizations { get; set; }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Worker_storekeeper>().ToTable("Worker_storekeepers");

            modelBuilder.Entity<Worker_storekeeper>()
                .HasOne(a => a.User)
                .WithOne(b => b.Worker_Storekeeper)
                .HasForeignKey<Worker_storekeeper>(b => b.User_id);
        }
    }
}
