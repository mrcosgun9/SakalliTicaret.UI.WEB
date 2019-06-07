using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class ProductTransaction:EntityLogBase
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int TransactionsType { get; set; }
    }
}
