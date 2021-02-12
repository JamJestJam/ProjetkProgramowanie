using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    public class Storage {
        #region Columns ======================================

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Storage_id { get; internal set; }

        [Required]
        public int Address_id { get; internal set; }

        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string Storage_name { get; internal set; }

        #endregion

        #region Fireign key ==================================

        public Address Address { get; }
        public IEnumerable<Product_order> Product_Order { get; }
        public IEnumerable<Storage_Product_localization> Storage_Product_Localizations { get; }

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Storage>().ToTable("Storages");

            modelBuilder.Entity<Storage>()
                .HasOne(a => a.Address)
                .WithMany(b => b.Storage_Addresses)
                .HasForeignKey(a => a.Address_id);

            modelBuilder.Entity<Storage>()
                .HasData(new Storage() {
                    Storage_id = 1,
                    Address_id = 1,
                    Storage_name = "Pierwotny magazyn"
                });
        }
    }
}
