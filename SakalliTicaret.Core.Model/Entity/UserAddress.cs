﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class UserAddress : EntityBase
    {
        [Display(Name = "Adres Sahibi")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Display(Name = "Başlık")]
        [Required]
        public string Title { get; set; }
        [Display(Name = "Şehir")]
        [Required]
        public string City { get; set; }
        [Display(Name = "Adres")]
        [Required]
        public string Address { get; set; }
        [Display(Name = "Telefon")]
        [Required]
        public string Phone { get; set; }
        [Display(Name = "Aktif")]
        public bool IsActice { get; set; }
        [Display(Name = "Varsayılan")]
        public bool IsDefault { get; set; }
        public virtual IEnumerable<User> Users { get; set; }

    }
}
