using MvcSalesApp.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MvcSalesApp.Data
{
  public class CustomerData
    {
    public List<Customer> GetAllCustomers() {
      using (var context = new OrderSystemContext()) {
        return context.Customers.AsNoTracking().ToList();
      }
  }

    public Customer FindCustomer(int? id) {
      using (var context = new OrderSystemContext()) {
        return context.Customers
          .AsNoTracking()
          .SingleOrDefault(c => c.CustomerId == id);
      }
    }

    public void AddCustomer(Customer customer) {
      using (var context=new OrderSystemContext()) {
        context.Customers.Add(customer);
        context.SaveChanges();
      }
    }

    public void UpdateCustomer(Customer customer) {
      using (var context = new OrderSystemContext()) {
        context.Entry(customer).State = EntityState.Modified;
        context.SaveChanges();
      }
    }

    public void RemoveCustomer(int id) {
      using (var context = new OrderSystemContext()) {
        context.Customers.Remove(context.Customers.Find(id));
        context.SaveChanges();
      }
    }
  }
}
