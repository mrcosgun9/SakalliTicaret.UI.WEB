namespace SakalliTicaret.Core.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LogProducts
    {
        public int ID { get; set; }

        public DateTime CreateDateTime { get; set; }

        public int CreateUserID { get; set; }

        public string Actions { get; set; }

        public string Name { get; set; }

        public int CategoryID { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Definition { get; set; }

        public decimal Price { get; set; }

        public decimal Tax { get; set; }

        public decimal Discount { get; set; }

        public int Stock { get; set; }

        public bool IsActive { get; set; }

        public virtual Categories Categories { get; set; }
    }
}
