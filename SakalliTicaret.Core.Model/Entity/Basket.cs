using System.Collections.Generic;

namespace SakalliTicaret.Core.Model.Entity
{
    public class Basket:EntityBase
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public string BasketKey { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public virtual IEnumerable<OrderProduct> OrderProducts { get; set; }
    }
}
