using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    public class Address {
        #region Columns ======================================

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Address_id { get; internal set; }

        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string Address_country { get; internal set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nchar")]
        public string Address_city { get; internal set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nchar")]
        public string Address_street { get; internal set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nchar")]
        public string Address_building_number { get; internal set; }

        [StringLength(50)]
        [Column(TypeName = "nchar")]
        public string Address_zip_code { get; internal set; }

        #endregion

        #region Fireign key ==================================

        public ICollection<User_address> User_Addresses { get; } = new List<User_address>();
        public ICollection<Storage> Storage_Addresses { get; } = new List<Storage>();

        #endregion

        #region Cuts =========================================

        public string Country => Address_country.Trim();
        public string City => Address_city.Trim();
        public string Street => Address_street.Trim();
        public string BuildingNumber => Address_building_number.Trim();
        public string ZipCode => Address_zip_code.Trim();

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Address>().ToTable("Addresses");
        }
    }
}
