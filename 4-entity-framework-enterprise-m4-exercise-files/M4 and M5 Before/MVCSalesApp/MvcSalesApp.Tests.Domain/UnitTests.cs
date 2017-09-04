using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcSalesApp.Domain;
using MvcSalesApp.Data;

namespace MvcSalesApp.Tests.Domain
{
  [TestClass]
  public class UnitTests
  {
    [TestMethod, TestCategory("Product")]
    public void NewProductIsAvailable() {
      Assert.IsTrue((new Product()).IsAvailable);
    }

    [TestMethod, TestCategory("Product")]
    public void NewProductWithDataIsAvailable() {
      var product = new Product
      {
        Name = "test",
        Description = "testing",
        ProductionStart = DateTime.Today
      };
      Assert.IsTrue(product.IsAvailable);
    }

    [TestMethod, TestCategory("Product")]
    public void CanDisableProduct() {
      var product = new Product();
      product.RemoveFromProduction();
      Assert.IsFalse(product.IsAvailable);
    }

    [TestMethod, TestCategory("ShoppingCart")]
    public void CanCreateRevisitedCartWithNoItems()
    {
      var cart = RevisitedCart.Create(1);

      Assert.AreEqual(1,cart.CartId);
    }
    [TestMethod, TestCategory("ShoppingCart")]
    public void CanCreateNewCartFromProductSelectionWithKnownCustomer() {
      var cart = NewCart.CreateCartFromProductSelection("http://www.thedatafarm.com","customerCookieString",1,1,9.99m);
      Assert.AreEqual(9.99m, cart.CartItems.Single().CurrentPrice);
    }

    [TestMethod, TestCategory("ShoppingCart")]
    public void CanCreateNewCartFromProductSelectionWithNoKnownCustomer() {
      var cart = NewCart.CreateCartFromProductSelection("http://thedatafarm.com",null, 1, 1, 9.99m);
      Assert.AreEqual(9.99m, cart.CartItems.Single().CurrentPrice);
    }


    [TestMethod, TestCategory("ShoppingCart")]
    public void CanInsertItemIntoEmptyRevisitedCart() {
      var cart = RevisitedCart.Create(1);
      cart.InsertNewCartItem(1, 1, 9.99m);
      Assert.AreEqual(1,cart.CartItems.Count);
    }
    [TestMethod, TestCategory("ShoppingCart")]
    public void CanCreateRevisitedCartWithExistingItems() {
      var cart = RevisitedCart.CreateWithItems(1,new List<CartItem> {CartItem.Create(1, 1, 9.99m, 1)});
      Assert.AreEqual(1, cart.CartItems.Count);
      Assert.AreEqual(9.99m, cart.CartItems.Single().CurrentPrice);
    }

  }
}
