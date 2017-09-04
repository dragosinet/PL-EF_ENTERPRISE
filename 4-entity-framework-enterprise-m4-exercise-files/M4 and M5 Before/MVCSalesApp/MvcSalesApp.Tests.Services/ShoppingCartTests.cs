using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcSalesApp.Data;
using MvcSalesApp.Domain;
using MvcSalesApp.Services;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MvcSalesApp.Tests.Services
{
  [TestClass]
  public class WebSiteOrderingServiceTests
  {
    private StringBuilder _logBuilder = new StringBuilder();
    private string _log;
    private OrderSystemContext _context;
    private string theUri = "http://www.thedatafarm.com";

    public WebSiteOrderingServiceTests() {
      _context = new OrderSystemContext();

      Database.SetInitializer(new DropCreateDatabaseIfModelChanges<OrderSystemContext>());
    }

    [TestMethod]
    public void InitializeCartReturnsRevisitedCart() {
      //will add mocking later ...
      var service = new WebSiteOrderingService(new WebSiteOrderData(new OrderSystemContext()));
      Assert.IsInstanceOfType(service.ItemSelected(1, 1, 9.99m, theUri, null,0), typeof(RevisitedCart));
    }

    [TestMethod]
    public void InitializeCartWithUnknownCustomerStoresZeroInCustomerId() {
      //will add mocking later ...
      var service = new WebSiteOrderingService(new WebSiteOrderData(_context));
      SetupLogging();
      RevisitedCart cart = service.ItemSelected(1, 1, 9.99m, theUri, null,0);
      WriteLog();
      Assert.AreEqual(0, _context.Carts.Find(cart.CartId).CustomerId);
    }

    [TestMethod]
    public void InitializeCartWithKnownCustomerStoresValueInCustomerId() {
      //will add mocking later ...

      using (var separateContext = new OrderSystemContext()) {
        if (!separateContext.Customers.Any(c => c.CustomerCookie == "CustomerCookieABCDE")) {
          separateContext.Customers.Add(new Customer
          {
            CustomerCookie = "CustomerCookieABCDE",
            DateOfBirth = DateTime.Now,
            FirstName = "Julie",
            LastName = "Lerman"
          });
          separateContext.SaveChanges();
        }
      }
      var service = new WebSiteOrderingService(new WebSiteOrderData(_context));
      SetupLogging();
      RevisitedCart cart = service.ItemSelected(1, 1, 9.99m, theUri, "CustomerCookieABCDE",0);
      WriteLog();
      Assert.AreNotEqual(0, _context.Carts.Find(cart.CartId).CustomerId);
    }

    private void WriteLog() {
      Debug.WriteLine(_log);
    }

    private void SetupLogging() {
      _context.Database.Log = BuildLogString;
    }

    private void BuildLogString(string message) {
      _logBuilder.Append(message);
      _log = _logBuilder.ToString();
    }
  }
}