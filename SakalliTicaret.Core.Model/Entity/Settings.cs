using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class Settings : EntityBase
    {
        public string GoogleAnalytics { get; set; }
        public string Logo { get; set; }
        [Display(Name = "Tema Rengi")]
        public string ThemaColor { get; set; }

    }
}
