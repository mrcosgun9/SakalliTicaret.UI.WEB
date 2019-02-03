using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class OrderProductProperty : EntityBase
    {
        public int OrderProductId { get; set; }
        public OrderProduct OrderProduct { get; set; }
        public int PropertyPropertyValuesId { get; set; }
        public PropertyPropertyValues PropertyPropertyValues { get; set; }
    }
}
