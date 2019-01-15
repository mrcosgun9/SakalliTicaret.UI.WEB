using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class ProductProperty:EntityBase
    {
        public int ProductId { get; set; }
        public Product ProductType { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
