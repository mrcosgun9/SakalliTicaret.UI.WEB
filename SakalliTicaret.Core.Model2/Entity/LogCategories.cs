namespace SakalliTicaret.Core.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LogCategories
    {
        public int ID { get; set; }

        public DateTime CreateDateTime { get; set; }

        public int CreateUserID { get; set; }

        public string Actions { get; set; }

        public int? ParentId { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
