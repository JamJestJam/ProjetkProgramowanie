using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    public enum UserGroup{
        User = 1,
        Worker = 2,
        Admin = 3
    }

    public class User_group {
        #region Columns ======================================

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_group_id { get; internal set; }

        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string User_group_name { get; internal set; }

        #endregion

        #region Fireign key ==================================

        public ICollection<User> Users { get; } = new List<User>();

        #endregion

        #region Cut ==========================================

        public UserGroup Group =>
            (UserGroup)User_group_id;

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User_group>().ToTable("User_groups");

            modelBuilder.Entity<User_group>()
                .HasData(new User_group() {
                    User_group_id = 1,
                    User_group_name = "Klient"
                });
            modelBuilder.Entity<User_group>()
                .HasData(new User_group() {
                    User_group_id = 2,
                    User_group_name = "Pracownik"
                });
            modelBuilder.Entity<User_group>()
                .HasData(new User_group() {
                    User_group_id = 3,
                    User_group_name = "Administrator"
                });
        }
    }
}
