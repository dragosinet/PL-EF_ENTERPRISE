namespace MvcSalesApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shoppingcartadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        CartItemId = c.Int(nullable: false, identity: true),
                        CartCookie = c.String(),
                        CartId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        SelectedDateTime = c.DateTime(nullable: false),
                        CurrentPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartItemId)
                .ForeignKey("dbo.NewCarts", t => t.CartId, cascadeDelete: true)
                .ForeignKey("dbo.RevisitedCarts", t => t.CartId, cascadeDelete: true)
                .Index(t => t.CartId);
            
            CreateTable(
                "dbo.NewCarts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        CartCookie = c.String(),
                        Initialized = c.DateTime(nullable: false),
                        Expires = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartId);
            
            CreateTable(
                "dbo.RevisitedCarts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        CartCookie = c.String(),
                    })
                .PrimaryKey(t => t.CartId);
            
            DropTable("dbo.CustFromIEntities");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CustFromIEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.CartItems", "CartId", "dbo.RevisitedCarts");
            DropForeignKey("dbo.CartItems", "CartId", "dbo.NewCarts");
            DropIndex("dbo.CartItems", new[] { "CartId" });
            DropTable("dbo.RevisitedCarts");
            DropTable("dbo.NewCarts");
            DropTable("dbo.CartItems");
        }
    }
}
