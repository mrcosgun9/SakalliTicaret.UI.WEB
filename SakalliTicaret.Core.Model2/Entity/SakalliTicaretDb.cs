namespace SakalliTicaret.Core.Model.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SakalliTicaretDb : DbContext
    {
        public SakalliTicaretDb()
            : base("name=SakalliTicaretDb")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Baskets> Baskets { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<CategoryProperties> CategoryProperties { get; set; }
        public virtual DbSet<CategoryPropertyValues> CategoryPropertyValues { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<LogBaskets> LogBaskets { get; set; }
        public virtual DbSet<LogCategories> LogCategories { get; set; }
        public virtual DbSet<LogProducts> LogProducts { get; set; }
        public virtual DbSet<LogUsers> LogUsers { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<OrderPayments> OrderPayments { get; set; }
        public virtual DbSet<OrderProducts> OrderProducts { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Pages> Pages { get; set; }
        public virtual DbSet<PosEntegrations> PosEntegrations { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<SocialMedias> SocialMedias { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<UserAddresses> UserAddresses { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Baskets>()
                .HasMany(e => e.OrderProducts)
                .WithRequired(e => e.Baskets)
                .HasForeignKey(e => e.BasketId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categories>()
                .HasMany(e => e.Categories1)
                .WithOptional(e => e.Categories2)
                .HasForeignKey(e => e.ParentCategory_ID);

            modelBuilder.Entity<Categories>()
                .HasMany(e => e.CategoryProperties)
                .WithRequired(e => e.Categories)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categories>()
                .HasMany(e => e.LogProducts)
                .WithRequired(e => e.Categories)
                .HasForeignKey(e => e.CategoryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categories>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Categories)
                .HasForeignKey(e => e.CategoryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CategoryProperties>()
                .HasMany(e => e.CategoryPropertyValues)
                .WithRequired(e => e.CategoryProperties)
                .HasForeignKey(e => e.CategoryPropertyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orders>()
                .HasMany(e => e.OrderPayments)
                .WithRequired(e => e.Orders)
                .HasForeignKey(e => e.OrderID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orders>()
                .HasMany(e => e.OrderProducts)
                .WithOptional(e => e.Orders)
                .HasForeignKey(e => e.Order_ID);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.OrderProducts)
                .WithRequired(e => e.Products)
                .HasForeignKey(e => e.ProductID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Baskets)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserAddresses>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.UserAddresses)
                .HasForeignKey(e => e.UserAddressID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Baskets)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.OrderProducts)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.UserAddresses)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);
        }
    }
}
