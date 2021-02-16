using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    /// <summary>
    /// Zamówiony produkt
    /// </summary>
    public class User_order_product {
        #region Columns ======================================
        /// <summary>
        /// Klucz główny
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_order_Products_id { get; internal set; }
        /// <summary>
        /// ID zamówienia
        /// </summary>
        [Required]
        public int User_order_id { get; internal set; }
        /// <summary>
        /// ID produktu
        /// </summary>
        [Required]
        public int Product_id { get; internal set; }
        /// <summary>
        /// Cena produktu
        /// </summary>
        [Required]
        [Column(TypeName = "smallmoney")]
        public decimal User_order_Product_price { get; internal set; }

        #endregion

        #region Fireign key ==================================
        /// <summary>
        /// Zamówienie
        /// </summary>
        public User_order Order { get; }
        /// <summary>
        /// Produkt
        /// </summary>
        public Product Product { get; }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User_order_product>().ToTable("User_order_Products");

            modelBuilder.Entity<User_order_product>()
                .HasOne(a => a.Order)
                .WithMany(b => b.Products)
                .HasForeignKey(a => a.User_order_id);

            modelBuilder.Entity<User_order_product>()
                .HasOne(a => a.Product)
                .WithMany(b => b.Order_Products)
                .HasForeignKey(a => a.Product_id);
        }
    }
}
