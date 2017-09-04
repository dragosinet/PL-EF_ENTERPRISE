using ShoppingCart.Domain;
using System.Data.Entity;

namespace ShoppingCart.Data
{
  public class ReferenceContext : DbContext
  {
    public ReferenceContext() : base("name=GeekStuffSales") {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      modelBuilder.HasDefaultSchema("Maintenance");
      modelBuilder.Entity<Product>().ToTable("ProductListing","ShoppingCart");
      //need a view:
      //  CREATE VIEW ShoppingCart.ProductListing
      //  AS
      //    SELECT ProductId, Description, P.Name, P.CategoryID,
      //           C.Name as Category, MaxQuantity, CurrentPrice
      //    FROM Maintenance.Products P
      //    LEFT Join Maintenance.Categories C
      //    ON P.CategoryId = C.CategoryId
      //    WHERE p.IsAvailable = 1

      base.OnModelCreating(modelBuilder);
    }
  }
}