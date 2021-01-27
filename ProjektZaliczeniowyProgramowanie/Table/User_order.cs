﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBconnectShop.Table {
    class User_order {
        #region Columns ======================================

        [Key]
        [Required]
        public int User_order_id { get; set; }

        [Required]
        public int User_order_status_id { get; set; }

        [Required]
        public int User_Address_id { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime User_order_date { get; set; }

        [Column(TypeName = "nchar")]
        [StringLength(100)]
        public string User_note { get; set; }

        #endregion

        #region Fireign key ==================================

        public User_order_status Order_Status { get; set; }
        public User_address Address { get; set; }
        public User_order_receipt Order_Receipt { get; set; }
        public IEnumerable<User_order_Product> Order_Products { get; set; } = new List<User_order_Product>();

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User_order>().ToTable("User_orders");

            modelBuilder.Entity<User_order>()
                .HasOne(a => a.Order_Status)
                .WithMany(b => b.User_Orders)
                .HasForeignKey(b=>b.User_order_status_id);

            modelBuilder.Entity<User_order>()
                .HasOne(a => a.Address)
                .WithMany(b => b.User_Orders)
                .HasForeignKey(b => b.User_Address_id);
        }
    }
}
