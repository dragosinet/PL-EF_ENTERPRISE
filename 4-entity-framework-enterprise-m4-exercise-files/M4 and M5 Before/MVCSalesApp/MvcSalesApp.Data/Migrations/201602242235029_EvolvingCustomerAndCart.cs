namespace MvcSalesApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EvolvingCustomerAndCart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewCarts", "CustomerCookie", c => c.String());
            AddColumn("dbo.Customers", "CustomerCookie", c => c.String());
            AddColumn("dbo.Products", "CurrentPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CurrentPrice");
            DropColumn("dbo.Customers", "CustomerCookie");
            DropColumn("dbo.NewCarts", "CustomerCookie");
        }
    }
}
