using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBconnectShop.Table {
    class Storage_Product_localization {
        #region Columns ======================================

        [Required]
        public int Storage_Product_id { get; set; }

        [Required]
        public int Storage_id { get; set; }

        [Required]
        public int Worker_storekeeper_id { get; set; }

        [Required]
        public bool Storage_Product_on_the_way { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime Storage_Product_date { get; set; }

        #endregion

        #region Fireign key ==================================

        public Storage_Product Storage_Product { get; set; }
        public Storage Storage { get; set; }
        public Worker_storekeeper Worker { get; set; }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Storage_Product_localization>().ToTable("Storage_Product_localizations");

            modelBuilder.Entity<Storage_Product_localization>()
                .HasKey(a=>new { a.Storage_Product_id, a.Storage_id });

            modelBuilder.Entity<Storage_Product_localization>()
                .HasOne(a => a.Storage_Product)
                .WithMany(b => b.Storage_Product_Localizations)
                .HasForeignKey(b => b.Storage_Product_id);

            modelBuilder.Entity<Storage_Product_localization>()
                .HasOne(a => a.Storage)
                .WithMany(b => b.Storage_Product_Localizations)
                .HasForeignKey(b => b.Storage_id);

            modelBuilder.Entity<Storage_Product_localization>()
                .HasOne(a => a.Worker)
                .WithMany(b => b.Storage_Product_Localizations)
                .HasForeignKey(b => b.Worker_storekeeper_id);
        }
    }
}
