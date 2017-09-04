namespace MvcSalesApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedProductABit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CategoryProducts", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.CategoryProducts", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.CategoryProducts", new[] { "Category_CategoryId" });
            DropIndex("dbo.CategoryProducts", new[] { "Product_ProductId" });
            AddColumn("dbo.Products", "CategoryId", c => c.Int(nullable: false,defaultValue:1));
            AddColumn("dbo.Products", "MaxQuantity", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "CategoryId");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
            DropTable("dbo.CategoryProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CategoryProducts",
                c => new
                    {
                        Category_CategoryId = c.Int(nullable: false),
                        Product_ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_CategoryId, t.Product_ProductId });
            
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropColumn("dbo.Products", "MaxQuantity");
            DropColumn("dbo.Products", "CategoryId");
            CreateIndex("dbo.CategoryProducts", "Product_ProductId");
            CreateIndex("dbo.CategoryProducts", "Category_CategoryId");
            AddForeignKey("dbo.CategoryProducts", "Product_ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.CategoryProducts", "Category_CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
    }
}
