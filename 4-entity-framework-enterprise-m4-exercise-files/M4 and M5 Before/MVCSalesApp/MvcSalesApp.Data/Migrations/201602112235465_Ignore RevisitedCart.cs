namespace MvcSalesApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IgnoreRevisitedCart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "CartId", "dbo.RevisitedCarts");
            DropTable("dbo.RevisitedCarts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RevisitedCarts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        CartCookie = c.String(),
                    })
                .PrimaryKey(t => t.CartId);
            
            AddForeignKey("dbo.CartItems", "CartId", "dbo.RevisitedCarts", "CartId", cascadeDelete: true);
        }
    }
}
