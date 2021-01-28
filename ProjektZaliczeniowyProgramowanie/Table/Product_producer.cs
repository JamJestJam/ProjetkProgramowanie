using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    class Product_producer {
        #region Columns ======================================

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_producer_id { get; set; }

        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string Product_producer_name { get; set; }

        #endregion

        #region Fireign key ==================================

        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product_producer>().ToTable("Product_producers");
        }
    }
}
