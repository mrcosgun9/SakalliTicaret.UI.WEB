using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class LogUsers : EntityLogBase
    {

        [Display(Name = "İsim")]
        public string Name { get; set; }

        [Display(Name = "Soyisim")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public int CreateUserId { get; set; }
        [Display(Name = "Telefon")]
        public string Telephone { get; set; }
        [Required]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Display(Name = "Tc Numarası")]
        public string TCKN { get; set; }
        [Display(Name = "Adresi")]
        public string Adress { get; set; }
    }
}
