using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    public class Address : IEquatable<Address> {
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

            modelBuilder.Entity<Address>()
                .HasIndex(a => new {
                    a.Address_city,
                    a.Address_country,
                    a.Address_street,
                    a.Address_building_number,
                    a.Address_zip_code
                })
                .IsUnique(true);

            modelBuilder.Entity<Address>()
                .HasData(new Address() {
                    Address_id = 1,
                    Address_country = "Polska",
                    Address_city = "Krakow",
                    Address_street = "Komandora Wrońskiego Bohdana",
                    Address_building_number = "54",
                    Address_zip_code = "30-852"
                });
        }

        public bool Equals(Address other) {
            if(Address_country != other.Address_country) return false;
            else if(Address_city != other.Address_city) return false;
            else if(Address_street != other.Address_street) return false;
            else if(Address_building_number != other.Address_building_number) return false;
            else if(Address_zip_code != other.Address_zip_code) return false;
            else return true;
        }

        public override bool Equals(object obj) =>
            (obj is Address address && Equals(address));

        public override int GetHashCode() =>
            HashCode.Combine(Address_id);
    }
}
