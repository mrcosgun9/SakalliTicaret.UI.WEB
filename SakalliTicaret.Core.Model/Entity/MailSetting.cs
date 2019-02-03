using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class MailSetting:EntityBase
    {
        [Display(Name = "Site Mail Adresi")]
        public string Mail { get; set; }
        [Display(Name = "Site Mail Şifresi")]
        public string Password { get; set; }
        [Display(Name = "Host Bilgisi")]
        public string Host { get; set; }
    }
}
