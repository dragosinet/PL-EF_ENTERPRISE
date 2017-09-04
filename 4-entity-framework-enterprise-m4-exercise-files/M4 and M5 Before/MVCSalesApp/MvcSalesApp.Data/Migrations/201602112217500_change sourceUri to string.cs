namespace MvcSalesApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesourceUritostring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewCarts", "SourceUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewCarts", "SourceUrl");
        }
    }
}
