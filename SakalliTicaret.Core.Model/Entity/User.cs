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
        [Required]
        public string Telephone { get; set; }
        [Required]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [MinLength(11,ErrorMessage = "En Az 11 Karakter Girmelisiniz.")]
        [Display(Name = "Tc Numarası")]
        [Required]
        public string TCKN { get; set; }
        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }

        [Display(Name = "Yönetici")]
        public bool IsAdmin { get; set; } = false;
        public string UserKey { get; set; }
        public bool IsMailSuccess { get; set; } = false;
        public UserCreateMethod CreateMethod { get; set; } = UserCreateMethod.UserCreate;
        public virtual IEnumerable<UserAddress> UserAddress { get; set; }
    }
    public enum UserCreateMethod
    {
        NotUserCreate = 0,
        UserCreate = 1
    }
}
