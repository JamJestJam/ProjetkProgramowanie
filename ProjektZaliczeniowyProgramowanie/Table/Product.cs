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
        /// <summary>
        /// Klucz główny
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_id { get; internal set; }
        /// <summary>
        /// ID kategorii
        /// </summary>
        [Required]
        public int Product_category_id { get; internal set; }
        /// <summary>
        /// Nazwa produktu
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column(TypeName = "nchar")]
        public string Product_name { get; internal set; }
        /// <summary>
        /// Dostępność produktu
        /// </summary>
        [Required]
        public bool Product_aviable { get; internal set; }

        #endregion

        #region Fireign key ==================================
        /// <summary>
        /// Kategoria produktu
        /// </summary>
        public Product_categori Product_Categori { get; internal set; }
        /// <summary>
        /// Oceny produktu
        /// </summary>
        public IEnumerable<Product_rating> Product_Ratings { get; } = new List<Product_rating>();
        /// <summary>
        /// Zdjęcia produktu
        /// </summary>
        public IEnumerable<Product_image> Product_Images { get; } = new List<Product_image>();
        /// <summary>
        /// Opinie produktu
        /// </summary>
        public IEnumerable<Product_opinion> Product_Opinions { get; } = new List<Product_opinion>();
        /// <summary>
        /// Specyfikacja produktu
        /// </summary>
        public IEnumerable<Product_specification> Product_Specifications { get; } = new List<Product_specification>();
        /// <summary>
        /// Historia cen produktu
        /// </summary>
        public IEnumerable<Products_price> Products_Prices { get; } = new List<Products_price>();
        /// <summary>
        /// Zamówienia produktu
        /// </summary>
        public IEnumerable<User_order_product> Order_Products { get; } = new List<User_order_product>();

        #endregion

        #region Cuts =========================================
        /// <summary>
        /// ID produktu
        /// </summary>
        public int ID => Product_id;
        /// <summary>
        /// Aktualna cena produktu
        /// </summary>
        public decimal ActualPrice {
            get {
                var price = Products_Prices
                    .Where(a => a.Product_price_date <= DateTime.Now)
                    .OrderBy(a => a.Product_price_date);

                var tmp = price.LastOrDefault();

                if(tmp is null)
                    return 0;
                return tmp.Product_price;
            }
        }
        /// <summary>
        /// Aktywne zdjęcia produktu
        /// </summary>
        /// <returns>Zwraca listę zdjęc</returns>
        public List<Product_image> TrueImages() =>
            Product_Images.Where(a => a.Product_image_active).ToList();
        /// <summary>
        /// Pierwsze aktywne zdjęcie
        /// </summary>
        public Image FirstImage {
            get {
                if(TrueImages().Count == 0)
                    return Image.Default;
                return TrueImages().First().Image;
            }
        }
        /// <summary>
        /// Skrócona nazwa produktu
        /// </summary>
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
                .HasData(new Product() {
                    Product_id = 1,
                    Product_category_id = 1,
                    Product_name = "Przykład"
                });
        }
    }
}
