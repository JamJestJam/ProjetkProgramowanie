using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    /// <summary>
    /// Stany zamówień
    /// </summary>
    public class User_order_status {
        #region Columns ======================================
        /// <summary>
        /// Klucz główny
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_order_status_id { get; internal set; }

        /// <summary>
        /// Nazwa stanu
        /// </summary>
        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string User_order_status_name { get; internal set; }

        #endregion

        #region Fireign key ==================================
        /// <summary>
        /// Lista zamówień w danym stanie
        /// </summary>
        public IEnumerable<User_order> User_Orders { get; } = new List<User_order>();

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User_order_status>().ToTable("User_order_status");

            modelBuilder.Entity<User_order_status>()
                .HasData(new User_order_status() {
                    User_order_status_id = 1,
                    User_order_status_name = "Złożone"
                });
            modelBuilder.Entity<User_order_status>()
                .HasData(new User_order_status() {
                    User_order_status_id = 2,
                    User_order_status_name = "Wykonane"
                });
            modelBuilder.Entity<User_order_status>()
                .HasData(new User_order_status() {
                    User_order_status_id = 3,
                    User_order_status_name = "Zrealizowane"
                });
            modelBuilder.Entity<User_order_status>()
               .HasData(new User_order_status() {
                   User_order_status_id = 4,
                   User_order_status_name = "Zwrucone"
               });
        }
    }
}
