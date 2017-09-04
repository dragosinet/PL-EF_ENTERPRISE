using MvcSalesApp.Domain;
using System.Collections.Generic;
using System.Linq;

namespace MvcSalesApp.Data
{
  public class CustomerWithOrdersData
  {
    OrderSystemContext _context;
    public CustomerWithOrdersData(OrderSystemContext context) {
      _context = context;
    }

    //public CustomerWithOrdersData() {
    //}

    public List<CustomerViewModel> GetAllCustomers() {
            return _context.Customers.AsNoTracking()
        .Select(c => new CustomerViewModel
        {
          CustomerId = c.CustomerId,
          Name = c.FirstName + " " + c.LastName,
          OrderCount = c.Orders.Count()
        })
        .ToList();
      }
  
    public CustomerViewModel FindCustomer(int? id) {
     
        var cust =
          _context.Customers.AsNoTracking()
                           .Select(c => new CustomerViewModel
                           {
                             CustomerId = c.CustomerId,
                             Name = c.FirstName + " " + c.LastName,
                             OrderCount = c.Orders.Count(),
                             Orders = c.Orders.Select(
                               o => new OrderViewModel
                               {
                                 OrderSource = o.OrderSource,
                                 CustomerId = o.CustomerId,
                                 OrderDate = o.OrderDate
                               }).ToList()
                           })
                            .FirstOrDefault(c => c.CustomerId == id);
        return cust;
      }
    }

   
  }

