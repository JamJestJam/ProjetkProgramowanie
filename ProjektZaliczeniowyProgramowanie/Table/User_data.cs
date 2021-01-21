using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DBconnectShop.Table {
    class User_data {
        #region Columns ======================================

        [Required]
        [Key]
        public int User_id { get; set; }

        [Required]
        [StringLength(25)]
        public string User_first_name { get; set; }

        [StringLength(25)]
        public string User_second_name { get; set; }

        [Required]
        [StringLength(25)]
        public string User_family_name { get; set; }

        #endregion

        #region Fireign key ==================================

        public virtual User User { get; set; }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User_data>().ToTable("Users_data");

            modelBuilder.Entity<User_data>()
                .HasOne(a => a.User)
                .WithOne(b => b.User_Data)
                .HasForeignKey<User_data>(b => b.User_id);
        }
    }
}
