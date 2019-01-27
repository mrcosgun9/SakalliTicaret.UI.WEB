using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class ProductImages : EntityBase
    {
        public int ProductId { get; set; }
        [Display(Name = "Ürün")]
        public Product Product { get; set; }
        [Display(Name = "Resim Yolu")]
        public string ImagesUrl { get; set; }
    }
}
