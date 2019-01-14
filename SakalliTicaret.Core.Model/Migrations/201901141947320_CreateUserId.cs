namespace SakalliTicaret.Core.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Baskets", new[] { "CreateUserId" });
            DropIndex("dbo.Users", new[] { "CreateUserId" });
            DropIndex("dbo.Status", new[] { "CreateUserId" });
            DropIndex("dbo.Categories", new[] { "CreateUserId" });
            DropIndex("dbo.CategoryProperties", new[] { "CreateUserId" });
            DropIndex("dbo.CategoryPropertyValues", new[] { "CreateUserId" });
            DropIndex("dbo.Contacts", new[] { "CreateUserId" });
            DropIndex("dbo.Messages", new[] { "CreateUserId" });
            DropIndex("dbo.OrderPayments", new[] { "CreateUserId" });
            DropIndex("dbo.OrderProducts", new[] { "CreateUserId" });
            DropIndex("dbo.Products", new[] { "CreateUserId" });
            DropIndex("dbo.Orders", new[] { "CreateUserId" });
            DropIndex("dbo.UserAddresses", new[] { "CreateUserId" });
            DropIndex("dbo.Pages", new[] { "CreateUserId" });
            DropIndex("dbo.PosEntegrations", new[] { "CreateUserId" });
            DropIndex("dbo.SocialMedias", new[] { "CreateUserId" });
            AlterColumn("dbo.Baskets", "CreateUserId", c => c.Int());
            AlterColumn("dbo.Users", "CreateUserId", c => c.Int());
            AlterColumn("dbo.Status", "CreateUserId", c => c.Int());
            AlterColumn("dbo.Categories", "CreateUserId", c => c.Int());
            AlterColumn("dbo.CategoryProperties", "CreateUserId", c => c.Int());
            AlterColumn("dbo.CategoryPropertyValues", "CreateUserId", c => c.Int());
            AlterColumn("dbo.Contacts", "CreateUserId", c => c.Int());
            AlterColumn("dbo.Messages", "CreateUserId", c => c.Int());
            AlterColumn("dbo.OrderPayments", "CreateUserId", c => c.Int());
            AlterColumn("dbo.OrderProducts", "CreateUserId", c => c.Int());
            AlterColumn("dbo.Products", "CreateUserId", c => c.Int());
            AlterColumn("dbo.Orders", "CreateUserId", c => c.Int());
            AlterColumn("dbo.UserAddresses", "CreateUserId", c => c.Int());
            AlterColumn("dbo.Pages", "CreateUserId", c => c.Int());
            AlterColumn("dbo.PosEntegrations", "CreateUserId", c => c.Int());
            AlterColumn("dbo.SocialMedias", "CreateUserId", c => c.Int());
            CreateIndex("dbo.Baskets", "CreateUserId");
            CreateIndex("dbo.Users", "CreateUserId");
            CreateIndex("dbo.Status", "CreateUserId");
            CreateIndex("dbo.Categories", "CreateUserId");
            CreateIndex("dbo.CategoryProperties", "CreateUserId");
            CreateIndex("dbo.CategoryPropertyValues", "CreateUserId");
            CreateIndex("dbo.Contacts", "CreateUserId");
            CreateIndex("dbo.Messages", "CreateUserId");
            CreateIndex("dbo.OrderPayments", "CreateUserId");
            CreateIndex("dbo.OrderProducts", "CreateUserId");
            CreateIndex("dbo.Products", "CreateUserId");
            CreateIndex("dbo.Orders", "CreateUserId");
            CreateIndex("dbo.UserAddresses", "CreateUserId");
            CreateIndex("dbo.Pages", "CreateUserId");
            CreateIndex("dbo.PosEntegrations", "CreateUserId");
            CreateIndex("dbo.SocialMedias", "CreateUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SocialMedias", new[] { "CreateUserId" });
            DropIndex("dbo.PosEntegrations", new[] { "CreateUserId" });
            DropIndex("dbo.Pages", new[] { "CreateUserId" });
            DropIndex("dbo.UserAddresses", new[] { "CreateUserId" });
            DropIndex("dbo.Orders", new[] { "CreateUserId" });
            DropIndex("dbo.Products", new[] { "CreateUserId" });
            DropIndex("dbo.OrderProducts", new[] { "CreateUserId" });
            DropIndex("dbo.OrderPayments", new[] { "CreateUserId" });
            DropIndex("dbo.Messages", new[] { "CreateUserId" });
            DropIndex("dbo.Contacts", new[] { "CreateUserId" });
            DropIndex("dbo.CategoryPropertyValues", new[] { "CreateUserId" });
            DropIndex("dbo.CategoryProperties", new[] { "CreateUserId" });
            DropIndex("dbo.Categories", new[] { "CreateUserId" });
            DropIndex("dbo.Status", new[] { "CreateUserId" });
            DropIndex("dbo.Users", new[] { "CreateUserId" });
            DropIndex("dbo.Baskets", new[] { "CreateUserId" });
            AlterColumn("dbo.SocialMedias", "CreateUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.PosEntegrations", "CreateUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Pages", "CreateUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserAddresses", "CreateUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "CreateUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "CreateUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderProducts", "CreateUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderPayments", "CreateUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Messages", "CreateUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Contacts", "CreateUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.CategoryPropertyValues", "CreateUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.CategoryProperties", "CreateUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "CreateUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Status", "CreateUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "CreateUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Baskets", "CreateUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.SocialMedias", "CreateUserId");
            CreateIndex("dbo.PosEntegrations", "CreateUserId");
            CreateIndex("dbo.Pages", "CreateUserId");
            CreateIndex("dbo.UserAddresses", "CreateUserId");
            CreateIndex("dbo.Orders", "CreateUserId");
            CreateIndex("dbo.Products", "CreateUserId");
            CreateIndex("dbo.OrderProducts", "CreateUserId");
            CreateIndex("dbo.OrderPayments", "CreateUserId");
            CreateIndex("dbo.Messages", "CreateUserId");
            CreateIndex("dbo.Contacts", "CreateUserId");
            CreateIndex("dbo.CategoryPropertyValues", "CreateUserId");
            CreateIndex("dbo.CategoryProperties", "CreateUserId");
            CreateIndex("dbo.Categories", "CreateUserId");
            CreateIndex("dbo.Status", "CreateUserId");
            CreateIndex("dbo.Users", "CreateUserId");
            CreateIndex("dbo.Baskets", "CreateUserId");
        }
    }
}
