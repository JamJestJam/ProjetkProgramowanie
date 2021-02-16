using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    /// <summary>
    /// Zamówienia klienta
    /// </summary>
    public class User_order {
        #region Columns ======================================
        /// <summary>
        /// klucz główny
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_order_id { get; internal set; }
        /// <summary>
        /// ID statusu zamówienia
        /// </summary>
        [Required]
        public int User_order_status_id { get; internal set; }
        /// <summary>
        /// Addres zamówienia
        /// </summary>
        [Required]
        public int User_Address_id { get; internal set; }
        /// <summary>
        /// Data zamówienia
        /// </summary>
        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime User_order_date { get; internal set; }

        #endregion

        #region Fireign key ==================================
        /// <summary>
        /// Stan zamówienia
        /// </summary>
        public User_order_status Order_Status { get; }
        /// <summary>
        /// Address zamówienia
        /// </summary>
        public User_address Address { get; }
        /// <summary>
        /// Produkty zamówione
        /// </summary>
        public IEnumerable<User_order_product> Products { get; internal set; } = new List<User_order_product>();

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User_order>().ToTable("User_orders");

            modelBuilder.Entity<User_order>()
                .Property(a => a.User_order_date)
                .HasDefaultValueSql("SYSDATETIME()");

            modelBuilder.Entity<User_order>()
                .HasOne(a => a.Order_Status)
                .WithMany(b => b.User_Orders)
                .HasForeignKey(b => b.User_order_status_id);

            modelBuilder.Entity<User_order>()
                .HasOne(a => a.Address)
                .WithMany(b => b.User_Orders)
                .HasForeignKey(b => b.User_Address_id);
        }
    }
}
