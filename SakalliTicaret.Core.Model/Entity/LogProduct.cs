using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class LogProduct:EntityLogBase
    {
        [Display(Name = "Başlık")]
        public string Name { get; set; }
        [Display(Name = "Kategori")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        [Display(Name = "Marka")]
        public string Brand { get; set; }
        [Display(Name = "Model")]
        public string Model { get; set; }
        [Display(Name = "Resim")]
        public string ImageUrl { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Detaylı Açıklama")]
        public string Definition { get; set; }
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }
        [Display(Name = "Vergi")]
        public decimal Tax { get; set; }
        [Display(Name = "İndirim")]
        public decimal Discount { get; set; }
        [Display(Name = "Stok")]
        public int Stock { get; set; }
        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
    }
}
