using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    class User_group {
        #region Columns ======================================

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_group_id { get; set; }

        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string User_group_name { get; set; }

        #endregion

        #region Fireign key ==================================

        public ICollection<User> Users { get; set; } = new List<User>();

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User_group>().ToTable("User_groups");
        }
    }
}
