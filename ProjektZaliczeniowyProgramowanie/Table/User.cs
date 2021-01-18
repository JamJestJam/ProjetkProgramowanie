using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjektZaliczeniowyProgramowanie.Table {
    class User {
        #region Columns ======================================

        [Required]
        [Key]
        public int User_id { get; set; }

        [Required]
        [StringLength(25)]
        public string User_name { get; set; }

        [Required]
        [StringLength(25)]
        public string User_password { get; set; }

        [Required]
        public bool User_active { get; set; }

        #endregion

        #region Fireign key ==================================

        [Required]
        public int User_group_id { get; set; }

        [Required]
        public virtual ICollection<User_group> User_Group { get; set; }

        #endregion

        public User() {
            User_Group = new HashSet<User_group>();
        }
    }
}
