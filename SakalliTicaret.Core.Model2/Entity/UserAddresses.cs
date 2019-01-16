namespace SakalliTicaret.Core.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserAddresses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserAddresses()
        {
            Orders = new HashSet<Orders>();
        }

        public int ID { get; set; }

        public int UserID { get; set; }

        public string Title { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public bool IsActice { get; set; }

        public int CreateUserID { get; set; }

        public DateTime? UpdateDateTime { get; set; }

        public int? UpdateUserID { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string Phone { get; set; }

        public bool IsDefault { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }

        public virtual Users Users { get; set; }
    }
}
