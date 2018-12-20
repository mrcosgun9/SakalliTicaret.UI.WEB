using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class Contact : EntityBase
    {
        [Display(Name = "Telefon-1")]
        public string Phone { get; set; }
        [Display(Name = "Telefon-2")]
        public string Phone1 { get; set; }
        [Display(Name = "Mail-1")]
        public string Mail { get; set; }
        [Display(Name = "Mail-2")]
        public string Mail2 { get; set; }
        [Display(Name = "Adres")]
        public string Adrress { get; set; }
    }
}
