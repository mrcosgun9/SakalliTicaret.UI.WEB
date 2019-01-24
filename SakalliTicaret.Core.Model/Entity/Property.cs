using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class Property:EntityBase
    {
        [Display(Name = "Ürün Özelliği")]
        public string Name { get; set; }


        public int ProductId { get; set; }
    }
}
