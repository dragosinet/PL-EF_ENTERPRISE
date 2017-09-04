using MvcSalesApp.Domain;
using System.Data.Entity;

namespace MvcSalesApp.Data
{
  public class OrderSystemContext : DbContext
  {
    public OrderSystemContext() : base("name=OrderSystemContext") {
    }

    public virtual DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
  }
}