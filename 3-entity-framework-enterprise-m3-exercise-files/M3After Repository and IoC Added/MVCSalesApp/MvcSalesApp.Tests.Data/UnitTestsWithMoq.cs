using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcSalesApp.Data;
using MvcSalesApp.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using DisconnectedGenericRepository;

namespace MvcSalesApp.Tests.Data
{
  [TestClass]
  public class UnitTestsWithMoq {
    [TestMethod]
    public void GetAllCustomersReturnsViewModel() {
      //arrange
      var customer = new Customer {
        FirstName = "Julie", LastName = "Lerman",
        Orders = new List<Order> { new Order() }
      };
      IQueryable<Customer> customers = (new List<Customer> { customer }).AsQueryable();
      var mockContext = new Mock<OrderSystemContext>();
      Mock<DbSet<Customer>> queryableMockSet = GenericSetupQueryableMockSet(customers);
      mockContext.Setup(c => c.Customers.AsNoTracking()).Returns(queryableMockSet.Object);
      //act
      var repo = new GenericRepository<Customer>(mockContext.Object);
      //assert
      Assert.IsInstanceOfType(repo.All().FirstOrDefault(), typeof(CustomerViewModel));
    }

    private static Mock<DbSet<T>> GenericSetupQueryableMockSet<T>(IQueryable<T> data) where T : class {
      var mockSet = new Mock<DbSet<T>>();
      mockSet.As<IQueryable<T>>().Setup(m => m.Provider)
        .Returns(data.Provider);
      mockSet.As<IQueryable<T>>().Setup(m => m.Expression)
        .Returns(data.Expression);
      mockSet.As<IQueryable<T>>().Setup(m => m.ElementType)
        .Returns(data.ElementType);
      mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator())
        .Returns(data.GetEnumerator());
      return mockSet;
    }
  }
}
