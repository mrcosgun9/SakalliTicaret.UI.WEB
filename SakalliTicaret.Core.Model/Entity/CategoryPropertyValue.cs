using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class CategoryPropertyValue : EntityBase
    {

        public int CategoryPropertyId { get; set; }
        public CategoryProperty CategoryProperty { get; set; }
        [Required]
        [Display(Name = "Değer")]
        public string Value { get; set; }

        public bool IsSelected { get; set; }

    }
}
