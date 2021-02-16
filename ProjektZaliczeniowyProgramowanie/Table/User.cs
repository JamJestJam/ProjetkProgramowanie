using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    /// <summary>
    /// Uzytkownik
    /// </summary>
    public class User {
        #region Columns ======================================
        /// <summary>
        /// Klucz główny
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_id { get; internal set; }
        /// <summary>
        /// Nazwa uzytkownika
        /// </summary>
        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string User_name { get; internal set; }
        /// <summary>
        /// Hasło uzytkownika
        /// </summary>
        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string User_password { get; internal set; }
        /// <summary>
        /// Czy uzytkownik jest zdolny do zalogowania
        /// </summary>
        [Required]
        public bool User_active { get; internal set; }
        /// <summary>
        /// ID grupy uzytkownika
        /// </summary>
        [Required]
        public int User_group_id { get; internal set; }

        #endregion

        #region Fireign key ==================================
        /// <summary>
        /// Grupa uzytkownika
        /// </summary>
        public User_group User_Group { get; }
        /// <summary>
        /// Dane o uzytkowniku
        /// </summary>
        public User_data User_Data { get; internal set; }
        /// <summary>
        /// Lista addresów uzytkonika
        /// </summary>
        public ICollection<User_address> User_Address { get; } = new List<User_address>();
        /// <summary>
        /// Lista wystawionych przez uzytkownika komentarzy
        /// </summary>
        public ICollection<Product_opinion> Product_Opinions { get; } = new List<Product_opinion>();
        /// <summary>
        /// Lista wystawiony przez uzytkownika ocen
        /// </summary>
        public ICollection<Product_rating> Product_Ratings { get; } = new List<Product_rating>();

        #endregion

        #region Cuts =========================================

        /// <summary>
        /// Skrócona nazwa użytkownika
        /// </summary>
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
