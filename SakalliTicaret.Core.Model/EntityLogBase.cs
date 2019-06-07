using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.Core.Model
{
    public class EntityLogBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Eklenme Tarihi")]
        public DateTime CreateDateTime { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int CreateUserId { get; set; }
        public User CreateUser { get; set; }

        [Display(Name = "Yapılan İşlem")]
        public string Actions { get; set; }

    }
}
