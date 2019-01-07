using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class OrderProduct : EntityBase
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        public bool InTheBasket { get; set; }
    }
}
