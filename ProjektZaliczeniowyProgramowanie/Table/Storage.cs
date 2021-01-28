﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    class Storage {
        #region Columns ======================================

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Storage_id { get; set; }

        [Required]
        public int Address_id { get; set; }

        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string Storage_name { get; set; }

        #endregion

        #region Fireign key ==================================

        public Address Address { get; set; }
        public IEnumerable<Product_order> Product_Order { get; set; }
        public IEnumerable<Storage_Product_localization> Storage_Product_Localizations { get; set; }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Storage>().ToTable("Storages");

            modelBuilder.Entity<Storage>()
                .HasOne(a => a.Address)
                .WithMany(b => b.Storage_Addresses)
                .HasForeignKey(a => a.Address_id);
        }
    }
}
