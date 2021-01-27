using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    class Product {
        #region Columns ======================================

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_id { get; set; }

        [Required]
        public int Product_category_id { get; set; }

        [Required]
        public int Product_producer_id { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName ="nchar")]
        public string Product_name { get; set; }

        [Required]
        [Column(TypeName = "ntext")]
        public string Product_description { get; set; }

        [Required]
        public bool Product_aviable { get; set; }

        #endregion

        #region Fireign key ==================================

        public Product_categori Product_Categori { get; set; }
        public Product_producer Product_Producer { get; set; }
        public IEnumerable<Storage_Product> Storage_Products { get; set; } = new List<Storage_Product>();
        public IEnumerable<Product_order> Product_Order { get; set; } = new List<Product_order>();
        public IEnumerable<Product_rating> Product_Ratings { get; set; } = new List<Product_rating>();
        public IEnumerable<Product_image> Product_Images { get; set; } = new List<Product_image>();
        public IEnumerable<Product_opinion> Product_Opinions { get; set; } = new List<Product_opinion>();
        public IEnumerable<Product_specification> Product_Specifications { get; set; } = new List<Product_specification>();
        public IEnumerable<Products_price> Products_Prices { get; set; } = new List<Products_price>();

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product>().ToTable("Products");

            modelBuilder.Entity<Product>()
                .Property(a => a.Product_aviable)
                .HasDefaultValue(true);

            modelBuilder.Entity<Product>()
                .HasOne(a => a.Product_Categori)
                .WithMany(b => b.Products)
                .HasForeignKey(b => b.Product_category_id);

            modelBuilder.Entity<Product>()
                .HasOne(a => a.Product_Producer)
                .WithMany(b => b.Products)
                .HasForeignKey(b => b.Product_producer_id);
        }
    }
}
