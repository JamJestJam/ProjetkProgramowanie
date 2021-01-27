using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    class User_order_status {
        #region Columns ======================================

        [Key]
        [Required]
        public int User_order_status_id { get; set; }

        [Required]
        [Column(TypeName = "nchar")]
        [StringLength(25)]
        public string User_order_status_name { get; set; }

        #endregion

        #region Fireign key ==================================

        public IEnumerable<User_order> User_Orders { get; set; } = new List<User_order>();

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User_order_status>().ToTable("User_order_status");
        }
    }
}
