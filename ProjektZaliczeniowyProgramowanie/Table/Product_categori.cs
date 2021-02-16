using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    /// <summary>
    /// Kategorie produktów
    /// </summary>
    public class Product_categori {
        #region Columns ======================================
        /// <summary>
        /// Klucz główny
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_category_id { get; internal set; }
        /// <summary>
        /// rodzic kategorii
        /// </summary>
        public int? Product_sub_category { get; internal set; }
        /// <summary>
        /// Nazwa kategorii
        /// </summary>
        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string Product_category_name { get; internal set; }

        #endregion

        #region Fireign key ==================================
        /// <summary>
        /// Rodzic kategorii
        /// </summary>
        public Product_categori Parent { get; }
        /// <summary>
        /// Lista dzieci kategorii
        /// </summary>
        public IEnumerable<Product_categori> Children { get; } = new List<Product_categori>();
        /// <summary>
        /// Lista produktów zawierających daną kategorie
        /// </summary>
        public IEnumerable<Product> Products { get; } = new List<Product>();

        #endregion

        #region Cuts =========================================

        /// <summary>
        /// Skrócone ID
        /// </summary>
        public int ID => Product_category_id;
        /// <summary>
        /// Skrócona ID rodzica
        /// </summary>
        public int ParentID => (Product_sub_category is null) ? 0 : (int)Product_sub_category;
        /// <summary>
        /// Skrócona nazwa produktu
        /// </summary>
        public string TrueName => Product_category_name.Trim();

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product_categori>().ToTable("Product_categories");

            modelBuilder.Entity<Product_categori>()
                .HasMany(a => a.Children)
                .WithOne(b => b.Parent)
                .HasForeignKey(b => b.Product_sub_category);

            modelBuilder.Entity<Product_categori>()
                .HasData(new Product_categori() {
                    Product_category_id = 1,
                    Product_category_name = "Przykład"
                });
        }
    }
}
