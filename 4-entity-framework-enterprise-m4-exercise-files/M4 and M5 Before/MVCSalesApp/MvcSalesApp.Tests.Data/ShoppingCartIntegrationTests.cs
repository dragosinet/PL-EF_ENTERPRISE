using DisconnectedGenericRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcSalesApp.Data;
using MvcSalesApp.Domain;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MvcSalesApp.Tests.Data
{
  [TestClass]
  public class ShoppingCartIntegrationTests
  {
    private readonly string _theUri = "http://www.thedatafarm.com";
    private StringBuilder _logBuilder = new StringBuilder();
    private string _log;
    private OrderSystemContext _context;
 
    public ShoppingCartIntegrationTests() {
      Database.SetInitializer(new DropCreateDatabaseIfModelChanges<OrderSystemContext>());
     _context=new OrderSystemContext();
      _context.Database.Initialize(force:true);//get this out of the way before logging
      SetupLogging();
    }

    [TestMethod]
    public void CanAddNewCartWithProductToCartsDbSet() {
      var cart = NewCart.CreateCartFromProductSelection(_theUri, null, 1, 1, 9.99m);
      _context.Carts.Add(cart);
      Assert.AreEqual(1, _context.Carts.Local.Count);

    }
    [TestMethod]
    public void CanStoreCartWithInitialProduct() {
   
      var cart = NewCart.CreateCartFromProductSelection(_theUri, null, 1, 1, 9.99m);
      var data = new WebSiteOrderData(_context);
      var resultCart = data.StoreCartWithInitialProduct(cart);
      WriteLog();
      Assert.AreNotEqual(0, resultCart.CartId);

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