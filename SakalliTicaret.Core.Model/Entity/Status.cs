using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class Status:EntityBase
    {
        [Display(Name = "Sipariş Durumu")]
        public string Name { get; set; }
        public virtual IEnumerable<Basket> Baskets{ get; set; }
    }
}
