using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class MailDesign : EntityBase
    {
        [Display(Name = "Başlık")]
        public string Name { get; set; }
        [Display(Name = "İçerik")]
        public string Content { get; set; }
        [Display(Name = "Taslak Türü")]
        public _MailType MailType { get; set; }
        public enum _MailType
        {
            RegisterLogin = 0
        }
    }
}
