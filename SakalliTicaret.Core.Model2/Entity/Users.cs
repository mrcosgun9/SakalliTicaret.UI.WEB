namespace SakalliTicaret.Core.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Baskets = new HashSet<Baskets>();
            OrderProducts = new HashSet<OrderProducts>();
            Orders = new HashSet<Orders>();
            UserAddresses = new HashSet<UserAddresses>();
        }

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Telephone { get; set; }

        [Required]
        public string Password { get; set; }

        public string TCKN { get; set; }

        public bool IsActive { get; set; }

        public bool IsAdmin { get; set; }

        public string ImageUrl { get; set; }

        public int CreateUserID { get; set; }

        public DateTime? UpdateDateTime { get; set; }

        public int? UpdateUserID { get; set; }

        public DateTime CreateDateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Baskets> Baskets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderProducts> OrderProducts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserAddresses> UserAddresses { get; set; }
    }
}
