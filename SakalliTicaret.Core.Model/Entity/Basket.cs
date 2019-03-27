using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SakalliTicaret.Core.Model.Entity
{
    public class Basket : EntityBase
    {
       

        [Display(Name = "Miktar")]
        public int Quantity { get; set; }
        [Display(Name = "Tutar")]
        public decimal Amount { get; set; }
        [Display(Name = "Sipariş Numarası")]
        public string BasketKey { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }

        public int? UserAddressId { get; set; }
        public UserAddress UserAddress { get; set; }

        public virtual List<OrderProduct> OrderProducts { get; set; }
        public virtual List<UserAddress> UserAddresses { get; set; }
    }
}
