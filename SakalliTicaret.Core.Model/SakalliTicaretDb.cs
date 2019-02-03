using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.Core.Model
{
    public class SakalliTicaretDb : DbContext
    {
        //public SakalliTicaretDb() : base("Data Source=sql2012.isimtescil.net;Initial Catalog=st1830298_db1234;Persist Security Info=True;User ID=st1830298_usr123;Password=Cr729ce_")
        //{

        //}
        public SakalliTicaretDb() : base("Data Source=.;Initial Catalog=st1830298_db1234;Integrated Security=True")
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderPayment> OrderPayments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<SocialMedia> SocialMediae { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Messages> Messageses { get; set; }
        public DbSet<PosEntegration> PosEntegrations { get; set; }
        public DbSet<CategoryProperty> CategoryProperties { get; set; }
        public DbSet<CategoryPropertyValue> CategoryPropertyValues { get; set; }
        public DbSet<PropertyPropertyValues> PropertyPropertyValueses { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
        public DbSet<ProductImages> ProductImageses { get; set; }
        public DbSet<OrderProductProperty> OrderProductProperties { get; set; }
        public DbSet<MailSetting> MailSettings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
