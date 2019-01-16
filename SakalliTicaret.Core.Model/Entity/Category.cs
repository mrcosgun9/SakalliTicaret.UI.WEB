using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SakalliTicaret.Core.Model.Entity
{
    public class Category : EntityBase
    {
        public int? ParentCategoryId { get; set; }
        [Display(Name = "Üst Kategori")]
        public Category ParentCategory { get; set; }
        // objenin ismi neyse sonuna ID getirmen lazım Foreign Key olması için
        // ya da üstte ki gibi yapabilirsin.
        // = 0 ne için?? 
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }
        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
        public virtual IEnumerable<CategoryProperty> CategoryProperties { get; set; }
        public virtual IEnumerable<Category> Categories { get; set; }


    }
}
