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
        [Required]
        [Display(Name = "İsim")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Soyisim")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Email adresi geçersiz")]
        public string Email { get; set; }
        [Display(Name = "Resim")]
        public string ImageUrl { get; set; }
        [Display(Name = "Telefon")]
        public string Telephone { get; set; }
        [Required]
        [StringLength(5)]
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
