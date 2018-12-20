using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class SocialMedia:EntityBase
    {
        [Display(Name = "İcon")]
        public string Icon { get; set; }
        [Display(Name = "Adı")]
        public string Name { get; set; }
        [Display(Name = "Url")]
        public string Url { get; set; }
    }
}
