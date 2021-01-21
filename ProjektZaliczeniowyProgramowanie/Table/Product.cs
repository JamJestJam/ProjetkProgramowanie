﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    class Product {
        #region Columns ======================================

        [Required]
        [Key]
        public int Product_id { get; set; }

        [Required]
        public int Product_category_id { get; set; }

        [Required]
        public int Product_producer_id { get; set; }

        [Required]
        [StringLength(50)]
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
        }
    }
}
