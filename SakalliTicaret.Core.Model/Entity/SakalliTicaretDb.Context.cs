﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SakalliTicaret.Core.Model.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SakalliTicaretDb : DbContext
    {
        public SakalliTicaretDb()
            : base("name=SakalliTicaretDb")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
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
    }
}