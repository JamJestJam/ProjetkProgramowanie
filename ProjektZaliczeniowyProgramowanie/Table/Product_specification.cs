using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    /// <summary>
    /// Specyfikacja produktu
    /// </summary>
    public class Product_specification {
        #region Columns ======================================
        /// <summary>
        /// Klucz główny
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_specification_id { get; internal set; }
        /// <summary>
        /// ID produktu
        /// </summary>
        [Required]
        public int Product_id { get; internal set; }
        /// <summary>
        /// Nazwa specyfikacji
        /// </summary>
        [Required]
        [StringLength(30)]
        [Column(TypeName = "nchar")]
        public string Product_specification_name { get; internal set; }
        /// <summary>
        /// Wartosc specyfikacji
        /// </summary>
        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string Product_specification_value { get; internal set; }

        #endregion

        #region Fireign key ==================================
        /// <summary>
        /// Produkt
        /// </summary>
        public Product Product { get; }

        #endregion

        #region Cuts =========================================

        public int ID => Product_specification_id;
        public string Name => Product_specification_name.Trim();
        public string Value => Product_specification_value.Trim();

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product_specification>().ToTable("Product_specifications");

            modelBuilder.Entity<Product_specification>()
                .HasOne(a => a.Product)
                .WithMany(b => b.Product_Specifications)
                .HasForeignKey(b => b.Product_id);
        }
    }
}
