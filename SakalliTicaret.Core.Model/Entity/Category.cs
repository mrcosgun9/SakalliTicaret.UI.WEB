using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SakalliTicaret.Core.Model.Entity
{
    public class Category : EntityBase
    {
        [Display(Name = "Üst Kategori")]
        public int ParentId { get; set; } = 0;
        public virtual Category ParentCategory { get; set; }
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }
        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
        public virtual IEnumerable<CategoryProperty> CategoryFeatureses { get; set; }
        public virtual IEnumerable<Category> Categories { get; set; }


    }
}
