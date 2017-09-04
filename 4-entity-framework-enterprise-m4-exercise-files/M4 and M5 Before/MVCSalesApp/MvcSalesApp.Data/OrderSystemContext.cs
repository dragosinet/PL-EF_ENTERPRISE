using MvcSalesApp.Domain;
using System.Data.Entity;

namespace MvcSalesApp.Data
{
  public class OrderSystemContext : DbContext
  {
    public OrderSystemContext() : base("name=OrderSystemContext") {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<NewCart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<NewCart>().HasKey(c => c.CartId);
      modelBuilder.Ignore<RevisitedCart>();
       base.OnModelCreating(modelBuilder);
    }
  }

  public class OrderSystemContextConfig : DbConfiguration
  {
    public OrderSystemContextConfig()
    {
       this.SetDatabaseInitializer(new NullDatabaseInitializer<OrderSystemContext>());
    }
    
  }
}