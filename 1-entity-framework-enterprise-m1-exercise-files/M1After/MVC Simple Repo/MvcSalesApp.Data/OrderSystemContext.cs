using MvcSalesApp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace MvcSalesApp.Data
{
  public class OrderSystemContext : DbContext
  {
    // You can add custom code to this file. Changes will not be overwritten.
    //
    // If you want Entity Framework to drop and regenerate your database
    // automatically whenever you change your model schema, please use data migrations.
    // For more information refer to the documentation:
    // http://msdn.microsoft.com/en-us/data/jj591621.aspx

    public OrderSystemContext() : base("name=OrderSystemContext") {
    }
    public DbSet<Product> Products { get; set; } //added this for the sake of seeding the database for the demo
    public DbSet<Customer> Customers { get; set; }
  }

  public class InitializerToSeedDataForDemo :CreateDatabaseIfNotExists<OrderSystemContext>
  {
    protected override void Seed(OrderSystemContext context) {
      var productlist = new[] {
        new Product {Description=@"32"" Curved Monitor", Name="Primo Monitor",ProductionStart=DateTime.Now, IsAvailable=true },
        new Product {Description=@"Super clicky keyboard", Name="Coder Keyboard",ProductionStart=DateTime.Now, IsAvailable=true },
        new Product {Description=@"Super ergo keyboard", Name="Comfy Keyboard",ProductionStart=DateTime.Now, IsAvailable=true },
        new Product {Description=@"Automatic Elevating Standing Desk", Name="Primo Desk",ProductionStart=DateTime.Now, IsAvailable=true },
        new Product {Description=@"Walk while you work", Name="Desk Treadmill",ProductionStart=DateTime.Now, IsAvailable=true },
        new Product {Description=@"Touch Screen Monitor", Name="Touchy Monitor",ProductionStart=DateTime.Now, IsAvailable=true }
        };

      var julie = new Customer { FirstName = "Julie", LastName = "Lerman", DateOfBirth = new DateTime(2010, 1, 1) };
      var julieOrders=new List<Order> {
        new Order { OrderDate = new DateTime(2015, 12, 25), OrderSource = Domain.Enums.OrderSource.Online,
                    LineItems = new[] {new LineItem {Quantity=6, ProductId=2},
                                       new LineItem {Quantity=7, ProductId=3} } },
        new Order { OrderDate = new DateTime(2015, 12, 24), OrderSource = Domain.Enums.OrderSource.InPerson,
                    LineItems=  new[] {new LineItem {Quantity=1, ProductId=3}} },
        new Order { OrderDate = new DateTime(2015, 12, 23), OrderSource = Domain.Enums.OrderSource.Email }
      };
      julieOrders.ForEach(o => julie.Orders.Add(o));
  
            var jason = new Customer { FirstName = "Jason", LastName = "Salmond", DateOfBirth = new DateTime(1985, 2, 1) };
      context.Products.AddRange(productlist);
      context.SaveChanges();
      context.Customers.AddRange(new[] { julie, jason });
    
      base.Seed(context);
    }
  }
}