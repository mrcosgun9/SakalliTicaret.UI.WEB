namespace SakalliTicaret.Core.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BasketKey = c.String(),
                        StatusId = c.Int(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.StatusId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        ImageUrl = c.String(),
                        Telephone = c.String(),
                        Password = c.String(nullable: false),
                        TCKN = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentCategoryId = c.Int(),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .ForeignKey("dbo.Categories", t => t.ParentCategoryId)
                .Index(t => t.ParentCategoryId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.CategoryProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Eligible = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .Index(t => t.CategoryId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.CategoryPropertyValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryPropertyId = c.Int(nullable: false),
                        Value = c.String(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoryProperties", t => t.CategoryPropertyId)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .Index(t => t.CategoryPropertyId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Phone = c.String(),
                        Phone1 = c.String(),
                        Mail = c.String(),
                        Mail2 = c.String(),
                        Adrress = c.String(),
                        IframeUrl = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.LogBaskets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BasketKey = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserID = c.Int(nullable: false),
                        Actions = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LogCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserID = c.Int(nullable: false),
                        Actions = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LogProducts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryID = c.Int(nullable: false),
                        Brand = c.String(),
                        Model = c.String(),
                        ImageUrl = c.String(),
                        Description = c.String(),
                        Definition = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserID = c.Int(nullable: false),
                        Actions = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.LogUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Telephone = c.String(),
                        Password = c.String(nullable: false),
                        TCKN = c.String(),
                        Adress = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserID = c.Int(nullable: false),
                        Actions = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SurName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        MessageTitle = c.String(),
                        Message = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.OrderPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        OrderType = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Bank = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .ForeignKey("dbo.Orders", t => t.OrderID)
                .Index(t => t.OrderID)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        BasketId = c.Int(nullable: false),
                        InTheBasket = c.Boolean(nullable: false),
                        Amount = c.Double(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserID = c.Int(),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baskets", t => t.BasketId)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.ProductId)
                .Index(t => t.UserId)
                .Index(t => t.BasketId)
                .Index(t => t.CreateUserId)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryID = c.Int(nullable: false),
                        Brand = c.String(),
                        Model = c.String(),
                        ImageUrl = c.String(),
                        Description = c.String(),
                        Definition = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .Index(t => t.CategoryID)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        UserAddressID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .ForeignKey("dbo.Status", t => t.StatusID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .ForeignKey("dbo.UserAddresses", t => t.UserAddressID)
                .Index(t => t.UserID)
                .Index(t => t.UserAddressID)
                .Index(t => t.StatusID)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.UserAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Title = c.String(),
                        City = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        IsActice = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Content = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DropDownMenu = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.PosEntegrations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreCode = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Installments = c.Int(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.SocialMedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Icon = c.String(),
                        Name = c.String(),
                        Url = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .Index(t => t.CreateUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SocialMedias", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.PosEntegrations", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.Pages", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "UserAddressID", "dbo.UserAddresses");
            DropForeignKey("dbo.UserAddresses", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserAddresses", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "UserID", "dbo.Users");
            DropForeignKey("dbo.Orders", "StatusID", "dbo.Status");
            DropForeignKey("dbo.OrderProducts", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.OrderPayments", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.OrderProducts", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrderProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.OrderProducts", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.OrderProducts", "BasketId", "dbo.Baskets");
            DropForeignKey("dbo.OrderPayments", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.Messages", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.LogProducts", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Contacts", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.CategoryPropertyValues", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.CategoryPropertyValues", "CategoryPropertyId", "dbo.CategoryProperties");
            DropForeignKey("dbo.CategoryProperties", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.CategoryProperties", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentCategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.Baskets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Baskets", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Status", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.Baskets", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.Users", "CreateUserId", "dbo.Users");
            DropIndex("dbo.SocialMedias", new[] { "CreateUserId" });
            DropIndex("dbo.PosEntegrations", new[] { "CreateUserId" });
            DropIndex("dbo.Pages", new[] { "CreateUserId" });
            DropIndex("dbo.UserAddresses", new[] { "CreateUserId" });
            DropIndex("dbo.UserAddresses", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "CreateUserId" });
            DropIndex("dbo.Orders", new[] { "StatusID" });
            DropIndex("dbo.Orders", new[] { "UserAddressID" });
            DropIndex("dbo.Orders", new[] { "UserID" });
            DropIndex("dbo.Products", new[] { "CreateUserId" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.OrderProducts", new[] { "Order_Id" });
            DropIndex("dbo.OrderProducts", new[] { "CreateUserId" });
            DropIndex("dbo.OrderProducts", new[] { "BasketId" });
            DropIndex("dbo.OrderProducts", new[] { "UserId" });
            DropIndex("dbo.OrderProducts", new[] { "ProductId" });
            DropIndex("dbo.OrderPayments", new[] { "CreateUserId" });
            DropIndex("dbo.OrderPayments", new[] { "OrderID" });
            DropIndex("dbo.Messages", new[] { "CreateUserId" });
            DropIndex("dbo.LogProducts", new[] { "CategoryID" });
            DropIndex("dbo.Contacts", new[] { "CreateUserId" });
            DropIndex("dbo.CategoryPropertyValues", new[] { "CreateUserId" });
            DropIndex("dbo.CategoryPropertyValues", new[] { "CategoryPropertyId" });
            DropIndex("dbo.CategoryProperties", new[] { "CreateUserId" });
            DropIndex("dbo.CategoryProperties", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "CreateUserId" });
            DropIndex("dbo.Categories", new[] { "ParentCategoryId" });
            DropIndex("dbo.Status", new[] { "CreateUserId" });
            DropIndex("dbo.Users", new[] { "CreateUserId" });
            DropIndex("dbo.Baskets", new[] { "CreateUserId" });
            DropIndex("dbo.Baskets", new[] { "StatusId" });
            DropIndex("dbo.Baskets", new[] { "UserId" });
            DropTable("dbo.SocialMedias");
            DropTable("dbo.PosEntegrations");
            DropTable("dbo.Pages");
            DropTable("dbo.UserAddresses");
            DropTable("dbo.Orders");
            DropTable("dbo.Products");
            DropTable("dbo.OrderProducts");
            DropTable("dbo.OrderPayments");
            DropTable("dbo.Messages");
            DropTable("dbo.LogUsers");
            DropTable("dbo.LogProducts");
            DropTable("dbo.LogCategories");
            DropTable("dbo.LogBaskets");
            DropTable("dbo.Contacts");
            DropTable("dbo.CategoryPropertyValues");
            DropTable("dbo.CategoryProperties");
            DropTable("dbo.Categories");
            DropTable("dbo.Status");
            DropTable("dbo.Users");
            DropTable("dbo.Baskets");
        }
    }
}
