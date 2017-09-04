namespace ShoppingCart.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddProductListingView : DbMigration
  {
    public override void Up() {
      this.Sql(
        @"CREATE VIEW ShoppingCart.ProductListing
          AS
          SELECT ProductId, Description, P.Name, P.CategoryID,
                 C.Name as Category, MaxQuantity, CurrentPrice
          FROM Maintenance.Products P
          LEFT Join Maintenance.Categories C
          ON P.CategoryId = C.CategoryId
          WHERE p.IsAvailable = 1"
      );
    }

    public override void Down() {
      Sql("DROP VIEW ShoppingCart.ProductListing");
    }
  }
}