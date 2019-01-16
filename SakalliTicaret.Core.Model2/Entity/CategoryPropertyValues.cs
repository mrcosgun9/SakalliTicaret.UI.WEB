namespace SakalliTicaret.Core.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CategoryPropertyValues
    {
        public int ID { get; set; }

        public int CategoryPropertyId { get; set; }

        [Required]
        public string Value { get; set; }

        public DateTime CreateDateTime { get; set; }

        public int CreateUserID { get; set; }

        public DateTime? UpdateDateTime { get; set; }

        public int? UpdateUserID { get; set; }

        public virtual CategoryProperties CategoryProperties { get; set; }
    }
}
