namespace SakalliTicaret.Core.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LogUsers
    {
        public DateTime CreateDateTime { get; set; }

        public int CreateUserID { get; set; }

        public string Actions { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        [Required]
        public string Password { get; set; }

        public string TCKN { get; set; }

        public string Adress { get; set; }
    }
}
