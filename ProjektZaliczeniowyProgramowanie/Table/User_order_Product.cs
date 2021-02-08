using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    public class User_order_Product {
        #region Columns ======================================

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_order_Product_id { get; internal set; }

        [Required]
        public int User_order_id { get; internal set; }

        [Required]
        public int Storage_Product_id { get; internal set; }

        [Required]
        [Column(TypeName = "smallmoney")]
        public decimal User_order_Product_price { get; internal set; }

        #endregion

        #region Fireign key ==================================

        public User_order Order { get;  }
        public Storage_Product Product { get;  }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User_order_Product>().ToTable("User_order_Products");

            modelBuilder.Entity<User_order_Product>()
                .HasOne(a => a.Order)
                .WithMany(b => b.Order_Products)
                .HasForeignKey(b => b.User_order_id);

            modelBuilder.Entity<User_order_Product>()
                .HasOne(a => a.Product)
                .WithMany(b => b.Order_Products)
                .HasForeignKey(b => b.Storage_Product_id);
        }
    }
}
