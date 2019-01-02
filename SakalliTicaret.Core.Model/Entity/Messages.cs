using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class Messages:EntityBase
    {
        [Display(Name = "İsim")]
        public string Name { get; set; }
        [Display(Name = "Soyisim")]
        public string SurName { get; set; }
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
        [Display(Name = "Mail")]
        public string Email { get; set; }
        [Display(Name = "Konu")]
        public string MessageTitle { get; set; }
        [Display(Name = "Mesaj")]
        public string Message { get; set; }

    }
}
