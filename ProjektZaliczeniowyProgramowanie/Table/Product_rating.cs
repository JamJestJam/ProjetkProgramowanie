using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBconnectShop.Table {
    class Product_rating {
        #region Columns ======================================

        [Required]
        public int Product_id { get; set; }

        [Required]
        public int User_id { get; set; }

        [Required]
        [Column(TypeName = "tinyint")]
        public short Product_Rating { get; set; }

        #endregion

        #region Fireign key ==================================

        public Product Product { get; set; }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product_rating>().ToTable("Product_images");

            modelBuilder.Entity<Product_rating>()
                .HasOne(a => a.Product)
                .WithMany(b => b.product_Ratings)
                .HasForeignKey(b => b.Product_id);
        }
    }
}
