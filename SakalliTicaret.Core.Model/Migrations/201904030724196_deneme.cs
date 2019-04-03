namespace SakalliTicaret.Core.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deneme : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasketCargoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BasketId = c.Int(nullable: false),
                        CargoCompanyId = c.Int(nullable: false),
                        TrackingNumber = c.String(),
                        DeliveryTime = c.Int(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Int(),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baskets", t => t.BasketId)
                .ForeignKey("dbo.CargoCompanies", t => t.CargoCompanyId)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .Index(t => t.BasketId)
                .Index(t => t.CargoCompanyId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.CargoCompanies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                        CreateUserId = c.Int(),
                        UpdateDateTime = c.DateTime(),
                        UpdateUserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .Index(t => t.CreateUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketCargoes", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.BasketCargoes", "CargoCompanyId", "dbo.CargoCompanies");
            DropForeignKey("dbo.CargoCompanies", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.BasketCargoes", "BasketId", "dbo.Baskets");
            DropIndex("dbo.CargoCompanies", new[] { "CreateUserId" });
            DropIndex("dbo.BasketCargoes", new[] { "CreateUserId" });
            DropIndex("dbo.BasketCargoes", new[] { "CargoCompanyId" });
            DropIndex("dbo.BasketCargoes", new[] { "BasketId" });
            DropTable("dbo.CargoCompanies");
            DropTable("dbo.BasketCargoes");
        }
    }
}
