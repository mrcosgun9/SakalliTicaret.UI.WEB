using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class Page : EntityBase
    {
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Display(Name = "Alt Başlık")]
        public string Description { get; set; }
        [Display(Name = "İçerik")]
        public string Content { get; set; }
        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
        [Display(Name = "Açılır Menü")]
        public bool DropDownMenu { get; set; }
    }
}
