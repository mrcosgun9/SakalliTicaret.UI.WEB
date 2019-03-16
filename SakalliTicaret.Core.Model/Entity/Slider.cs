using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class Slider : EntityBase
    {
        [Display(Name = "Resim")]
        public string ImageUrl { get; set; }
        [Display(Name = "Yönelendirme")]
        public bool IsRedirect { get; set; } = true;
        [Display(Name = "Yönlendirilecek Sayfa")]
        public string Url { get; set; }
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Link Metni")]
        public string ButtonText { get; set; }
    }
}
