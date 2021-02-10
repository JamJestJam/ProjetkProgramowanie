using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DBconnectShop.Table {
    public class User_order_product_storage {
        #region Columns ======================================

        [Key]
        [Required]
        public int User_order_Products_id { get; internal set; }

        [Required]
        public int Storage_Product_id { get; internal set; }

        #endregion

        #region Fireign key ==================================

        public Storage_Product Storage_Product { get; }
        public User_order_product Product { get; }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User_order_product_storage>().ToTable("User_order_Products_storage");

            modelBuilder.Entity<User_order_product_storage>()
                .HasOne(a => a.Storage_Product)
                .WithOne(b => b.Order)
                .HasForeignKey<User_order_product_storage>(a => a.Storage_Product_id);

            modelBuilder.Entity<User_order_product_storage>()
                .HasOne(a => a.Product)
                .WithOne(b => b.Storage)
                .HasForeignKey<User_order_product_storage>(a => a.User_order_Products_id);
        }
    }
}
