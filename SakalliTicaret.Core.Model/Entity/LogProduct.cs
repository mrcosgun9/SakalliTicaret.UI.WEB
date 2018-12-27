using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class LogProduct:EntityLogBase
    {
        public Product Urun { get; set; }
    }
}
