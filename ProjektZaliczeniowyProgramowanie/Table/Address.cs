using System.ComponentModel.DataAnnotations;

namespace DBconnectShop.Table {
    class Address {
        #region Columns ======================================

        [Required]
        [Key]
        public int Address_id { get; set; }

        [Required]
        [StringLength(25)]
        public string Address_country { get; set; }

        [Required]
        [StringLength(50)]
        public string Address_city { get; set; }

        [Required]
        [StringLength(50)]
        public string Address_street { get; set; }

        [Required]
        [StringLength(50)]
        public string Address_building_number { get; set; }

        [Required]
        [StringLength(50)]
        public string Address_zip_code { get; set; }

        #endregion
    }
}
