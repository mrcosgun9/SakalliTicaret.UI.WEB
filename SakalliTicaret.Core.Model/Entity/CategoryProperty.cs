using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class CategoryProperty : EntityBase
    {
        [Required]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        [Display(Name = "İsim")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Seçilebilir")]
        public bool Eligible { get; set; }
        public virtual IEnumerable<CategoryPropertyValue> CategoryPropertyValues { get; set; }
        



    }
}
