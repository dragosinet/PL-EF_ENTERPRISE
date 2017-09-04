using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcSalesApp.Domain;
using MvcSalesApp.Data;

namespace MvcSalesApp.Tests.Domain
{
  [TestClass]
  public class UnitTests
  {
    [TestMethod]
    public void NewProductIsAvailable() {
      Assert.IsTrue((new Product()).IsAvailable);
    }

    [TestMethod]
    public void NewProductWithDataIsAvailable() {
      var product = new Product
      {
        Name = "test",
        Description = "testing",
        ProductionStart = DateTime.Today
      };
      Assert.IsTrue(product.IsAvailable);
    }

    [TestMethod]
    public void CanDisableProduct() {
      var product = new Product();
      product.RemoveFromProduction();
      Assert.IsFalse(product.IsAvailable);
    }

  }
}
