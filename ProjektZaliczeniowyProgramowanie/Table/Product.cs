using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DBconnectShop.Table {
    public class Product {
        #region Columns ======================================

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_id { get; internal set; }

        [Required]
        public int Product_category_id { get; internal set; }

        [Required]
        public int Product_producer_id { get; internal set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nchar")]
        public string Product_name { get; internal set; }

        [Required]
        [Column(TypeName = "ntext")]
        public string Product_description { get; internal set; }

        [Required]
        public bool Product_aviable { get; internal set; }

        #endregion

        #region Fireign key ==================================

        public Product_categori Product_Categori { get; }
        public Product_producer Product_Producer { get; }
        public IEnumerable<Storage_Product> Storage_Products { get; } = new List<Storage_Product>();
        public IEnumerable<Product_order> Product_Order { get; } = new List<Product_order>();
        public IEnumerable<Product_rating> Product_Ratings { get; } = new List<Product_rating>();
        public IEnumerable<Product_image> Product_Images { get; } = new List<Product_image>();
        public IEnumerable<Product_opinion> Product_Opinions { get; } = new List<Product_opinion>();
        public IEnumerable<Product_specification> Product_Specifications { get; } = new List<Product_specification>();
        public IEnumerable<Products_price> Products_Prices { get; } = new List<Products_price>();
        public IEnumerable<User_order_product> Order_Products { get; } = new List<User_order_product>();

        #endregion

        #region Cuts =========================================

        public int ID => Product_id;
        public decimal ActualPrice => Products_Prices.Where(a => a.Product_price_date <= DateTime.Now).OrderBy(a => a.Product_price_date).FirstOrDefault().Product_price;
        public string TrueName => Product_name.Trim();

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
