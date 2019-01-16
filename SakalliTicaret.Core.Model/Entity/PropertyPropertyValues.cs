using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class PropertyPropertyValues : EntityBase
    {
        public int CategoryPropertyId { get; set; }
        public CategoryProperty CategoryProperty { get; set; }
        public int CategoryPropertyValueId { get; set; }
        public CategoryPropertyValue CategoryPropertyValue { get; set; }
    }
}
