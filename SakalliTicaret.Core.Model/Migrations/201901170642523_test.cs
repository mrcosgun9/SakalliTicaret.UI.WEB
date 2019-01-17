namespace SakalliTicaret.Core.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LogProducts", "CategoryID", "dbo.Categories");
            DropIndex("dbo.LogProducts", new[] { "CategoryID" });
            DropIndex("dbo.OrderProducts", new[] { "Order_ID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            RenameColumn(table: "dbo.Categories", name: "ParentCategory_ID", newName: "ParentCategoryID");
            RenameIndex(table: "dbo.Categories", name: "IX_ParentCategory_ID", newName: "IX_ParentCategoryID");
            CreateTable(
                "dbo.PropertyPropertyValues",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryPropertyID = c.Int(nullable: false),
                        CategoryPropertyValueID = c.Int(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserID = c.Int(),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CategoryProperties", t => t.CategoryPropertyID)
                .ForeignKey("dbo.CategoryPropertyValues", t => t.CategoryPropertyValueID)
                .ForeignKey("dbo.Users", t => t.CreateUserID)
                .Index(t => t.CategoryPropertyID)
                .Index(t => t.CategoryPropertyValueID)
                .Index(t => t.CreateUserID);
            
            AlterColumn("dbo.Baskets", "CreateUserID", c => c.Int());
            AlterColumn("dbo.Status", "CreateUserID", c => c.Int());
            AlterColumn("dbo.Users", "CreateUserID", c => c.Int());
            AlterColumn("dbo.Categories", "CreateUserID", c => c.Int());
            AlterColumn("dbo.CategoryProperties", "CreateUserID", c => c.Int());
            AlterColumn("dbo.CategoryPropertyValues", "CreateUserID", c => c.Int());
            AlterColumn("dbo.Contacts", "CreateUserID", c => c.Int());
            AlterColumn("dbo.LogProducts", "CategoryID", c => c.Int());
            AlterColumn("dbo.Messages", "CreateUserID", c => c.Int());
            AlterColumn("dbo.OrderPayments", "CreateUserID", c => c.Int());
            AlterColumn("dbo.OrderProducts", "CreateUserID", c => c.Int());
            AlterColumn("dbo.Products", "CategoryID", c => c.Int());
            AlterColumn("dbo.Products", "CreateUserID", c => c.Int());
            AlterColumn("dbo.Orders", "CreateUserID", c => c.Int());
            AlterColumn("dbo.UserAddresses", "CreateUserID", c => c.Int());
            AlterColumn("dbo.Pages", "CreateUserID", c => c.Int());
            AlterColumn("dbo.PosEntegrations", "CreateUserID", c => c.Int());
            AlterColumn("dbo.SocialMedias", "CreateUserID", c => c.Int());
            CreateIndex("dbo.Baskets", "CreateUserID");
            CreateIndex("dbo.Users", "CreateUserID");
            CreateIndex("dbo.Status", "CreateUserID");
            CreateIndex("dbo.Categories", "CreateUserID");
            CreateIndex("dbo.CategoryProperties", "CreateUserID");
            CreateIndex("dbo.CategoryPropertyValues", "CreateUserID");
            CreateIndex("dbo.Contacts", "CreateUserID");
            CreateIndex("dbo.Messages", "CreateUserID");
            CreateIndex("dbo.OrderPayments", "CreateUserID");
            CreateIndex("dbo.OrderProducts", "CreateUserID");
            CreateIndex("dbo.OrderProducts", "Order_ID");
            CreateIndex("dbo.Products", "CategoryID");
            CreateIndex("dbo.Products", "CreateUserID");
            CreateIndex("dbo.Orders", "CreateUserID");
            CreateIndex("dbo.UserAddresses", "CreateUserID");
            CreateIndex("dbo.Pages", "CreateUserID");
            CreateIndex("dbo.PosEntegrations", "CreateUserID");
            CreateIndex("dbo.SocialMedias", "CreateUserID");
            AddForeignKey("dbo.Users", "CreateUserID", "dbo.Users", "ID");
            AddForeignKey("dbo.Baskets", "CreateUserID", "dbo.Users", "ID");
            AddForeignKey("dbo.Status", "CreateUserID", "dbo.Users", "ID");
            AddForeignKey("dbo.Categories", "CreateUserID", "dbo.Users", "ID");
            AddForeignKey("dbo.CategoryProperties", "CreateUserID", "dbo.Users", "ID");
            AddForeignKey("dbo.CategoryPropertyValues", "CreateUserID", "dbo.Users", "ID");
            AddForeignKey("dbo.Contacts", "CreateUserID", "dbo.Users", "ID");
            AddForeignKey("dbo.Messages", "CreateUserID", "dbo.Users", "ID");
            AddForeignKey("dbo.OrderPayments", "CreateUserID", "dbo.Users", "ID");
            AddForeignKey("dbo.OrderProducts", "CreateUserID", "dbo.Users", "ID");
            AddForeignKey("dbo.Products", "CreateUserID", "dbo.Users", "ID");
            AddForeignKey("dbo.Orders", "CreateUserID", "dbo.Users", "ID");
            AddForeignKey("dbo.UserAddresses", "CreateUserID", "dbo.Users", "ID");
            AddForeignKey("dbo.Pages", "CreateUserID", "dbo.Users", "ID");
            AddForeignKey("dbo.PosEntegrations", "CreateUserID", "dbo.Users", "ID");
            AddForeignKey("dbo.SocialMedias", "CreateUserID", "dbo.Users", "ID");
            DropColumn("dbo.Categories", "ParentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "ParentID", c => c.Int(nullable: false));
            DropForeignKey("dbo.SocialMedias", "CreateUserID", "dbo.Users");
            DropForeignKey("dbo.PropertyPropertyValues", "CreateUserID", "dbo.Users");
            DropForeignKey("dbo.PropertyPropertyValues", "CategoryPropertyValueID", "dbo.CategoryPropertyValues");
            DropForeignKey("dbo.PropertyPropertyValues", "CategoryPropertyID", "dbo.CategoryProperties");
            DropForeignKey("dbo.PosEntegrations", "CreateUserID", "dbo.Users");
            DropForeignKey("dbo.Pages", "CreateUserID", "dbo.Users");
            DropForeignKey("dbo.UserAddresses", "CreateUserID", "dbo.Users");
            DropForeignKey("dbo.Orders", "CreateUserID", "dbo.Users");
            DropForeignKey("dbo.Products", "CreateUserID", "dbo.Users");
            DropForeignKey("dbo.OrderProducts", "CreateUserID", "dbo.Users");
            DropForeignKey("dbo.OrderPayments", "CreateUserID", "dbo.Users");
            DropForeignKey("dbo.Messages", "CreateUserID", "dbo.Users");
            DropForeignKey("dbo.Contacts", "CreateUserID", "dbo.Users");
            DropForeignKey("dbo.CategoryPropertyValues", "CreateUserID", "dbo.Users");
            DropForeignKey("dbo.CategoryProperties", "CreateUserID", "dbo.Users");
            DropForeignKey("dbo.Categories", "CreateUserID", "dbo.Users");
            DropForeignKey("dbo.Status", "CreateUserID", "dbo.Users");
            DropForeignKey("dbo.Baskets", "CreateUserID", "dbo.Users");
            DropForeignKey("dbo.Users", "CreateUserID", "dbo.Users");
            DropIndex("dbo.SocialMedias", new[] { "CreateUserID" });
            DropIndex("dbo.PropertyPropertyValues", new[] { "CreateUserID" });
            DropIndex("dbo.PropertyPropertyValues", new[] { "CategoryPropertyValueID" });
            DropIndex("dbo.PropertyPropertyValues", new[] { "CategoryPropertyID" });
            DropIndex("dbo.PosEntegrations", new[] { "CreateUserID" });
            DropIndex("dbo.Pages", new[] { "CreateUserID" });
            DropIndex("dbo.UserAddresses", new[] { "CreateUserID" });
            DropIndex("dbo.Orders", new[] { "CreateUserID" });
            DropIndex("dbo.Products", new[] { "CreateUserID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.OrderProducts", new[] { "Order_ID" });
            DropIndex("dbo.OrderProducts", new[] { "CreateUserID" });
            DropIndex("dbo.OrderPayments", new[] { "CreateUserID" });
            DropIndex("dbo.Messages", new[] { "CreateUserID" });
            DropIndex("dbo.Contacts", new[] { "CreateUserID" });
            DropIndex("dbo.CategoryPropertyValues", new[] { "CreateUserID" });
            DropIndex("dbo.CategoryProperties", new[] { "CreateUserID" });
            DropIndex("dbo.Categories", new[] { "CreateUserID" });
            DropIndex("dbo.Status", new[] { "CreateUserID" });
            DropIndex("dbo.Users", new[] { "CreateUserID" });
            DropIndex("dbo.Baskets", new[] { "CreateUserID" });
            AlterColumn("dbo.SocialMedias", "CreateUserID", c => c.Int(nullable: false));
            AlterColumn("dbo.PosEntegrations", "CreateUserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Pages", "CreateUserID", c => c.Int(nullable: false));
            AlterColumn("dbo.UserAddresses", "CreateUserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "CreateUserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "CreateUserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "CategoryID", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderProducts", "CreateUserID", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderPayments", "CreateUserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Messages", "CreateUserID", c => c.Int(nullable: false));
            AlterColumn("dbo.LogProducts", "CategoryID", c => c.Int(nullable: false));
            AlterColumn("dbo.Contacts", "CreateUserID", c => c.Int(nullable: false));
            AlterColumn("dbo.CategoryPropertyValues", "CreateUserID", c => c.Int(nullable: false));
            AlterColumn("dbo.CategoryProperties", "CreateUserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "CreateUserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "CreateUserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Status", "CreateUserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Baskets", "CreateUserID", c => c.Int(nullable: false));
            DropTable("dbo.PropertyPropertyValues");
            RenameIndex(table: "dbo.Categories", name: "IX_ParentCategoryID", newName: "IX_ParentCategory_ID");
            RenameColumn(table: "dbo.Categories", name: "ParentCategoryID", newName: "ParentCategory_ID");
            CreateIndex("dbo.Products", "CategoryID");
            CreateIndex("dbo.OrderProducts", "Order_ID");
            CreateIndex("dbo.LogProducts", "CategoryID");
            AddForeignKey("dbo.LogProducts", "CategoryID", "dbo.Categories", "ID");
        }
    }
}
