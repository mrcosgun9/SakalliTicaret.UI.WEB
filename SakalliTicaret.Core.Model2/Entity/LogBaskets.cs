namespace SakalliTicaret.Core.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LogBaskets
    {
        public int ID { get; set; }

        public DateTime CreateDateTime { get; set; }

        public int CreateUserID { get; set; }

        public string Actions { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Amount { get; set; }

        public string BasketKey { get; set; }
    }
}
