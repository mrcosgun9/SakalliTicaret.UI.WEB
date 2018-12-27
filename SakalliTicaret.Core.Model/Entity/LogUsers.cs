using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class LogUsers: EntityLogBase
    {
        public User User { get; set; }
    }
}
