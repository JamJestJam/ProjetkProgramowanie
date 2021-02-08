using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    public class User_order_status {
        #region Columns ======================================

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_order_status_id { get; internal set; }

        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string User_order_status_name { get; internal set; }

        #endregion

        #region Fireign key ==================================

        public IEnumerable<User_order> User_Orders { get; } = new List<User_order>();

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User_order_status>().ToTable("User_order_status");
        }
    }
}
