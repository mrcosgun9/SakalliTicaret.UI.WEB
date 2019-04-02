using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
   public class BasketCargo : EntityBase
    {
        [Display(Name = "Sepet")]
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        [Display(Name = "Kargo Firması")]
        public int CargoCompanyId { get; set; }
        public CargoCompany CargoCompany { get; set; }
        [Display(Name = "Takip Numarası")]
        public string TrackingNumber { get; set; }
        [Display(Name = "Tahmini Teslim Süresi")]
        public int DeliveryTime { get; set; }
    }
}
