namespace SakalliTicaret.Core.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contacts
    {
        public int ID { get; set; }

        public string Phone { get; set; }

        public string Phone1 { get; set; }

        public string Mail { get; set; }

        public string Mail2 { get; set; }

        public string Adrress { get; set; }

        public DateTime CreateDateTime { get; set; }

        public int CreateUserID { get; set; }

        public DateTime? UpdateDateTime { get; set; }

        public int? UpdateUserID { get; set; }

        public string IframeUrl { get; set; }
    }
}
