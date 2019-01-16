namespace SakalliTicaret.Core.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderProducts
    {
        public int ID { get; set; }

        public int? Order_ID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public int CreateUserID { get; set; }

        public DateTime? UpdateDateTime { get; set; }

        public int? UpdateUserID { get; set; }

        public DateTime CreateDateTime { get; set; }

        public int UserId { get; set; }

        public int BasketId { get; set; }

        public bool InTheBasket { get; set; }

        public double Amount { get; set; }

        public virtual Baskets Baskets { get; set; }

        public virtual Orders Orders { get; set; }

        public virtual Products Products { get; set; }

        public virtual Users Users { get; set; }
    }
}
