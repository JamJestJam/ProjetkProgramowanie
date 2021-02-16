using DBconnectShop.Addons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace DBconnectShop.Table {
    public class Product : IEquatable<Product> {
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
        public decimal ActualPrice {
            get {
                var tmp = Products_Prices
                    .Where(a => a.Product_price_date <= DateTime.Now)
                    .OrderBy(a => a.Product_price_date)
                    .FirstOrDefault();

                if(tmp is null)
                    return 0;
                return tmp.Product_price;
            }
        }
        public List<Product_image> TrueImages() =>
            Product_Images.Where(a => a.Product_image_active).ToList();
        public Image FirstImage {
            get {
                if(TrueImages().Count == 0)
                    return Image.Default;
                return TrueImages().First().Image;
            }
        }
        public string TrueName => Product_name.Trim();

        #endregion

        #region Equals =======================================

        public bool Equals(Product other) {
            if(other is null)
                return false;

            return this.ID == other.ID;
        }

        public override bool Equals(object obj) =>
            obj is Product product && Equals(product);

        public override int GetHashCode() =>
            HashCode.Combine(ID);

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product>().ToTable("Products");

            modelBuilder.Entity<Product>()
                .HasOne(a => a.Product_Categori)
                .WithMany(b => b.Products)
                .HasForeignKey(b => b.Product_category_id);

            modelBuilder.Entity<Product>()
                .HasOne(a => a.Product_Producer)
                .WithMany(b => b.Products)
                .HasForeignKey(b => b.Product_producer_id);

            modelBuilder.Entity<Product>()
                .HasData(new Product() {
                    Product_id = 1,
                    Product_producer_id = 1,
                    Product_category_id = 1,
                    Product_name = "Przykład"
                });
        }
    }
}
