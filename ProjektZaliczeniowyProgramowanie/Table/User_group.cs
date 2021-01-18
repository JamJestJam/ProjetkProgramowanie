using System.ComponentModel.DataAnnotations;

namespace ProjektZaliczeniowyProgramowanie.Table {
    class User_group {
        #region Columns ======================================

        [Required]
        [Key]
        public int User_group_id { get; set; }

        [Required]
        [StringLength(25)]
        public string User_group_name { get; set; }

        #endregion
    }
}
