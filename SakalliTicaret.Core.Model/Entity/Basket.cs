using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SakalliTicaret.Core.Model.Entity
{
    public class Basket:EntityBase
    {
        public int UserId { get; set; }
        public User User { get; set; }
        [Display(Name = "Miktar")]
        public int Quantity { get; set; }
        [Display(Name = "Tutar")]
        public decimal Amount { get; set; }
        [Display(Name = "Sipariş Numarası")]
        public string BasketKey { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public virtual IEnumerable<OrderProduct> OrderProducts { get; set; }
    }
}
