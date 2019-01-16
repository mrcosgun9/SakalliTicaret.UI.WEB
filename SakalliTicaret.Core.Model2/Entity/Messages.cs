namespace SakalliTicaret.Core.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Messages
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string MessageTitle { get; set; }

        public string Message { get; set; }

        public DateTime CreateDateTime { get; set; }

        public int CreateUserID { get; set; }

        public DateTime? UpdateDateTime { get; set; }

        public int? UpdateUserID { get; set; }
    }
}
