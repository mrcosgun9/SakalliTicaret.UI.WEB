namespace SakalliTicaret.Core.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            OrderPayments = new HashSet<OrderPayments>();
            OrderProducts = new HashSet<OrderProducts>();
        }

        public int ID { get; set; }

        public int UserID { get; set; }

        public int UserAddressID { get; set; }

        public int StatusID { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalTax { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal Total { get; set; }

        public int CreateUserID { get; set; }

        public DateTime? UpdateDateTime { get; set; }

        public int? UpdateUserID { get; set; }

        public DateTime CreateDateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderPayments> OrderPayments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderProducts> OrderProducts { get; set; }

        public virtual Status Status { get; set; }

        public virtual UserAddresses UserAddresses { get; set; }

        public virtual Users Users { get; set; }
    }
}
