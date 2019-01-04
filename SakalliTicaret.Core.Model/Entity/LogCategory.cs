using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class LogCategory:EntityLogBase
    {
        [Display(Name = "Üst Kategori")]
        public int? ParentId { get; set; } = 0;
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }
        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
    }
}
