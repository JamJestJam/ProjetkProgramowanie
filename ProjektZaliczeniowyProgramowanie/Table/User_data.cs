using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ImageAddon = DBconnectShop.Addons.Image;

namespace DBconnectShop.Table {
    /// <summary>
    /// Dane uzytkownika
    /// </summary>
    public class User_data {
        #region Columns ======================================
        /// <summary>
        /// ID uzytkownika, klucz główny
        /// </summary>
        [Key]
        [Required]
        public int User_id { get; internal set; }
        /// <summary>
        /// Imie
        /// </summary>
        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string User_first_name { get; internal set; }
        /// <summary>
        /// Drugie imie
        /// </summary>
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string User_second_name { get; internal set; }
        /// <summary>
        /// Nazwisko
        /// </summary>
        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string User_family_name { get; internal set; }
        /// <summary>
        /// Avatar uzytkownika
        /// </summary>
        [Column(TypeName = "varBinary(max)")]
        public byte[] User_avatar { get; internal set; }

        #endregion

        #region Fireign key ==================================
        /// <summary>
        /// Uzytkownik
        /// </summary>
        public User User { get; }

        #endregion

        #region Cuts =========================================
        /// <summary>
        /// Obrazek z avatara
        /// </summary>
        public ImageAddon Image => new ImageAddon(User_avatar);

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
