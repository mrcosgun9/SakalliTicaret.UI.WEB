using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakalliTicaret.Core.Model.Entity
{
    public class BasketTransactions : EntityBase
    {
        [Display(Name = "Sipariş")]
        public int? BasketId { get; set; }
        public Basket Basket { get; set; }
        [Display(Name = "Sipariş Numarası")]
        public string BasketKey { get; set; }
        [Display(Name = "Dönen Değer")]
        public string Status { get; set; }
        [Display(Name = "Kullanıcı")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
    }
}
