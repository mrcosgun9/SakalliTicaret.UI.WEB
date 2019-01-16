namespace SakalliTicaret.Core.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderPayments
    {
        public int ID { get; set; }

        public int OrderID { get; set; }

        public int OrderType { get; set; }

        public decimal Price { get; set; }

        public string Bank { get; set; }

        public int CreateUserID { get; set; }

        public DateTime? UpdateDateTime { get; set; }

        public int? UpdateUserID { get; set; }

        public DateTime CreateDateTime { get; set; }

        public virtual Orders Orders { get; set; }
    }
}
