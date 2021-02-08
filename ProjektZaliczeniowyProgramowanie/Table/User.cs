﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBconnectShop.Table {
    public class User {
        #region Columns ======================================

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_id { get; internal set; }

        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string User_name { get; internal set; }

        [Required]
        [StringLength(25)]
        [Column(TypeName = "nchar")]
        public string User_password { get; internal set; }

        [Required]
        public bool User_active { get; internal set; }

        [Required]
        public int User_group_id { get; internal set; }

        #endregion

        #region Fireign key ==================================

        public User_group User_Group { get; }
        public User_data User_Data { get; internal set; }
        public ICollection<User_address> User_Address { get;  } = new List<User_address>();
        public ICollection<Product_opinion> Product_Opinions { get;  } = new List<Product_opinion>();
        public ICollection<Product_rating> Product_Ratings { get;  } = new List<Product_rating>();
        public Worker_seller Worker_Seller { get;  }
        public Worker_storekeeper Worker_Storekeeper { get;  }
        public Worker_purchaser Worker_Purchaser { get;  }

        #endregion

        #region Cuts =========================================

        public string UserName => User_name;

        #endregion

        internal static void ModelCreate(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<User>()
                .Property(a => a.User_active)
                .HasDefaultValue(true);

            modelBuilder.Entity<User>()
                .Property(a => a.User_group_id)
                .HasDefaultValue(1);

            modelBuilder.Entity<User>()
                .HasOne(a => a.User_Group)
                .WithMany(b => b.Users)
                .HasForeignKey(b => b.User_group_id);
        }
    }
}
