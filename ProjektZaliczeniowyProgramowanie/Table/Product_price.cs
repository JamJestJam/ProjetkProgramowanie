﻿using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    /// <summary>
    /// cana produktu
    /// </summary>
    public class Products_price {
        #region Columns ======================================

        /// <summary>
        /// Klucz główny
        /// </summary>
        [Required]
        public int Product_id { get; internal set; }
        /// <summary>
        /// Data od której cena obowiązuje
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime Product_price_date { get; internal set; }
        /// <summary>
        /// Cena
        /// </summary>
        [Required]
        [Column(TypeName = "smallmoney")]
        public decimal Product_price { get; internal set; }

        #endregion

        #region Fireign key ==================================
        /// <summary>
        /// Produkt
        /// </summary>
        public Product Product { get; }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Products_price>().ToTable("Products_price");

            modelBuilder.Entity<Products_price>()
                .Property(a => a.Product_price_date)
                .HasDefaultValueSql("SYSDATETIME()");

            modelBuilder.Entity<Products_price>()
                .HasKey(a => new { a.Product_id, a.Product_price_date });

            modelBuilder.Entity<Products_price>()
                .HasOne(a => a.Product)
                .WithMany(b => b.Products_Prices)
                .HasForeignKey(b => b.Product_id);

            modelBuilder.Entity<Products_price>()
                .HasData(new Products_price() {
                    Product_id = 1,
                    Product_price_date = DateTime.Now,
                    Product_price = 100
                });
        }
    }
}
