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
    }
}