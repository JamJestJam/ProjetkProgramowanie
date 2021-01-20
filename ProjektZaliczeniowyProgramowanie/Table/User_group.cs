using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBconnectShop.Table {
    class User_group {
        #region Columns ======================================

        [Required]
        [Key]
        public int User_group_id { get; set; }

        [Required]
        [StringLength(25)]
        public string User_group_name { get; set; }

        #endregion

        #region Fireign key ==================================

        public ICollection<User> Users { get; set; } = new List<User>();

        #endregion

        public static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User_group>().ToTable("User_groups");
        }
    }
}
