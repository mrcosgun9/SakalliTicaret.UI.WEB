using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class PosEntegration : EntityBase
    {
        [Display(Name = "Mağaza Kodu")]
        public string StoreCode { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Display(Name = "Taksit")]
        public int Installments { get; set; }

        [Display(Name = "Test Modülü")]
        public bool TestModule { get; set; } = false;
    }
}
