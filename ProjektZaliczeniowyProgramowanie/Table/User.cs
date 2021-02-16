using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    public class User {
        #region Columns ======================================

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_id { get; internal set; }

        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string User_name { get; internal set; }

        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string User_password { get; internal set; }

        [Required]
        public bool User_active { get; internal set; }

        [Required]
        public int User_group_id { get; internal set; }

        #endregion

        #region Fireign key ==================================

        public User_group User_Group { get; }
        public User_data User_Data { get; internal set; }
        public ICollection<User_address> User_Address { get; } = new List<User_address>();
        public ICollection<Product_opinion> Product_Opinions { get; } = new List<Product_opinion>();
        public ICollection<Product_rating> Product_Ratings { get; } = new List<Product_rating>();

        #endregion

        #region Cuts =========================================

        public string UserName => (User_Data is null)
            ? User_name.Trim() :
            User_Data.User_first_name.Trim() + " " + User_Data.User_family_name.Trim();

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<User>()
                .Property(a => a.User_active)
                .HasDefaultValue(true);

            modelBuilder.Entity<User>()
                .Property(a => a.User_group_id)
                .HasDefaultValue(1);

            modelBuilder.Entity<User>()
                .HasOne(a => a.User_Group)
                .WithMany(b => b.Users)
                .HasForeignKey(b => b.User_group_id);

            modelBuilder.Entity<User>()
                .HasData(new User() {
                    User_id = 1,
                    User_group_id = 3,
                    User_name = "Admin",
                    User_password = "admin",
                    User_active = true
                });
        }
    }
}
