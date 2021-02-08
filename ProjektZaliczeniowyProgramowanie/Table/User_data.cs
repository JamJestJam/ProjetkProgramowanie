using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using ImageAddon = DBconnectShop.Addons.Image;

namespace DBconnectShop.Table {
    public class User_data {
        #region Columns ======================================

        [Key]
        [Required]
        public int User_id { get; internal set; }

        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string User_first_name { get; internal set; }

        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string User_second_name { get; internal set; }

        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string User_family_name { get; internal set; }

        [Column(TypeName = "varBinary")]
        public byte[] User_avatar { get; internal set; }

        #endregion

        #region Fireign key ==================================

        public User User { get; }

        #endregion

        #region Cuts =========================================

        public Bitmap Bitmap => new ImageAddon(User_avatar).bitmap;

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
