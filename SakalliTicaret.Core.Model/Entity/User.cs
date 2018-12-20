using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class User : EntityBase
    {
        [Display(Name = "İsim")]
        public string Name { get; set; }
        [Display(Name = "Soyisim")]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Resim")]
        public string ImageUrl { get; set; }
        [Display(Name = "Telefon")]
        public string Telephone { get; set; }
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Display(Name = "Tc Numarası")]
        public string TCKN { get; set; }
        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
        [Display(Name = "Yönetici")]
        public bool IsAdmin { get; set; }
        public virtual IEnumerable<UserAddress> UserAddress { get; set; }
    }
}
