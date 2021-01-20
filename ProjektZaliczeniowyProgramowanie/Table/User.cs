using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBconnectShop.Table  {
    class User {
        #region Columns ======================================

        [Required]
        [Key]
        public int User_id { get; set; }

        [Required]
        [StringLength(25)]
        public string User_name { get; set; }

        [Required]
        [StringLength(25)]
        public string User_password { get; set; }

        [Required]
        public bool User_active { get; set; }

        #endregion

        #region Fireign key ==================================

        [Required]
        public int User_group_id { get; set; }

        public virtual User_group User_Group { get; set; }

        public User_data User_Data { get; set; }

        public ICollection<User_address> User_Address { get; set; } = new List<User_address>();

        #endregion

        public static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<User>()
                .HasOne(a => a.User_Group)
                .WithMany(b => b.Users)
                .HasForeignKey(b => b.User_group_id);
        }
    }
}
