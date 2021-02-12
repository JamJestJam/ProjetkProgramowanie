using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    public class Product_producer {
        #region Columns ======================================

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_producer_id { get; internal set; }

        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string Product_producer_name { get; internal set; }

        #endregion

        #region Fireign key ==================================

        public IEnumerable<Product> Products { get; } = new List<Product>();

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product_producer>().ToTable("Product_producers");

            modelBuilder.Entity<Product_producer>()
                .HasData(new Product_producer() {
                    Product_producer_id = 1,
                    Product_producer_name = "Przykład"
                });
        }
    }
}
