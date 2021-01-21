using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBconnectShop.Table {
    class Product_categori {
        #region Columns ======================================

        [Key]
        [Required]
        public int Product_category_id { get; set; }

        public int? Product_sub_category { get; set; }

        [Required]
        [StringLength(25)]
        public string Product_category_name { get; set; }

        #endregion

        #region Fireign key ==================================

        public Product_categori Parent { get; set; }

        public IEnumerable<Product_categori> Children { get; set; } = new List<Product_categori>();

        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product_categori>().ToTable("Product_categories");

            modelBuilder.Entity<Product_categori>()
                .HasMany(a => a.Children)
                .WithOne(b => b.Parent)
                .HasForeignKey(b => b.Product_sub_category);
        }
    }
}
