using MvcSalesApp.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MvcSalesApp.Data
{
  public class ProductData
  {
    public List<Product> GetAllCustomers() {
      using (var context = new OrderSystemContext()) {
        return context.Products.AsNoTracking().OrderBy(p => p.Name).ToList();
      }
    }

    public Product FindProduct(int? id) {
      using (var context = new OrderSystemContext()) {
        return context.Products
          .AsNoTracking()
          .SingleOrDefault(p => p.ProductId == id);
      }
    }

    public void AddProduct(Product product) {
      using (var context = new OrderSystemContext()) {
        product.IsAvailable = true;
        context.Products.Add(product);
        context.SaveChanges();
      }
    }

    public void RemoveProduct(int id) {
      using (var context = new OrderSystemContext()) {
        context.Database
          .ExecuteSqlCommand
          ($"update products set isavailable=0 where productid={id}");
      }
    }


    //public Product RemoveProduct(int id) {
    //  using (var context = new OrderSystemContext()) {
    //    var product = context.Products.Find(id);
    //    product.RemoveFromProduction();
    //    context.SaveChanges();
    //    return product;
    //  }
    //}

    //public Product RemoveProduct(Product product) {
    //  using (var context = new OrderSystemContext()) {
    //    product.RemoveFromProduction();
    //    context.SaveChanges();
    //    return product;
    //  }
    //}
  }
}
