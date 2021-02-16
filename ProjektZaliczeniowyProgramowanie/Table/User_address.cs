using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    /// <summary>
    /// Połączenie tabel address i user
    /// </summary>
    public class User_address {
        #region Columns ======================================
        /// <summary>
        /// Klucz główny
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_Address_id { get; internal set; }
        /// <summary>
        /// ID addresu
        /// </summary>
        [Required]
        public int Address_id { get; internal set; }
        /// <summary>
        /// ID uzytkownika
        /// </summary>
        [Required]
        public int User_id { get; internal set; }

        #endregion

        #region Fireign key ==================================
        /// <summary>
        /// Address
        /// </summary>
        public Address Address { get; internal set; }
        /// <summary>
        /// Uzytkownik
        /// </summary>
        public User User { get; internal set; }
        /// <summary>
        /// Lista zamówień na dany address przez danego uzytkownika
        /// </summary>
        public IEnumerable<User_order> User_Orders { get; internal set; } = new List<User_order>();

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User_address>().ToTable("User_Addresses");

            modelBuilder.Entity<User_address>()
                .HasIndex(a => new {
                    a.Address_id,
                    a.User_id
                })
                .IsUnique(true);

            modelBuilder.Entity<User_address>()
                .HasOne(a => a.Address)
                .WithMany(b => b.User_Addresses)
                .HasForeignKey(b => b.Address_id);

            modelBuilder.Entity<User_address>()
                .HasOne(a => a.User)
                .WithMany(b => b.User_Address)
                .HasForeignKey(b => b.User_id);
        }
    }
}
