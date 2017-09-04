using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCart.Domain;

namespace ShoppingCart.Data.Tests
{
  [TestClass]
  public class ShoppingCartIntegrationTests
  {
    private readonly string _theUri = "http://www.thedatafarm.com";
    private ShoppingCartContext _context;
    private string _log;
    private StringBuilder _logBuilder = new StringBuilder();
    private ReferenceContext _refContext;

    public ShoppingCartIntegrationTests()
    {
      Database.SetInitializer(new NullDatabaseInitializer<ShoppingCartContext>());
      _context = new ShoppingCartContext();
      _refContext = new ReferenceContext();
      _context.Database.Initialize(true); //get this out of the way before logging
      SetupLogging();
    }

    [TestMethod]
    public void CanAddNewCartWithProductToCartsDbSet()
    {
      var cart = NewCart.CreateCartFromProductSelection(_theUri, null, 1, 1, 9.99m);
      _context.Carts.Add(cart);
      Assert.AreEqual(1, _context.Carts.Local.Count);
    }

    [TestMethod]
    public void CanStoreCartWithInitialProduct()
    {
      var cart = NewCart.CreateCartFromProductSelection(_theUri, null, 1, 1, 9.99m);
      var data = new WebSiteOrderData(_context, _refContext);
      var resultCart = data.StoreCartWithInitialProduct(cart);
      WriteLog();
      Assert.AreNotEqual(0, resultCart.CartId);
    }

    [TestMethod]
    public void CanUpdateCartItems()
    {
      //Arrange
      var goodId = _context.Carts.Where(c => c.CartItems.Any())
        .Select(c => c.CartId)
        .FirstOrDefault();
      var data1 = new WebSiteOrderData(new ShoppingCartContext(), _refContext);
      var existingCart = data1.RetrieveCart(goodId);
      var lineItemCount = existingCart.CartItems.Count();
      var firstItem = existingCart.CartItems.First();
      var originalTotalItems = existingCart.TotalItems;
      var originalQuantity = firstItem.Quantity;
      existingCart.CartItems.First().UpdateQuantity(originalQuantity + 1);
      existingCart.InsertNewCartItem(1, 1, new decimal(100));
      //Act
      var data2 = new WebSiteOrderData(new ShoppingCartContext(), _refContext);
      data2.UpdateItemsForExistingCart(existingCart);
      //Assert
      var data3 = new WebSiteOrderData(new ShoppingCartContext(), _refContext);
      var existingCartAgain = data3.RetrieveCart(goodId);
      Assert.AreEqual(lineItemCount + 1, existingCartAgain.CartItems.Count());
      Assert.AreEqual(originalTotalItems + 2, existingCartAgain.TotalItems);
    }

    [TestMethod]
    public void CanRetrieveCartFromRepoUsingCartId()
    {
      //Arrange
      var cart = NewCart.CreateCartFromProductSelection(_theUri, null, 1, 1, 9.99m);
      var data = new WebSiteOrderData(_context, _refContext);
      var createdCart = data.StoreCartWithInitialProduct(cart);
      Debug.WriteLine($"Stored Cart Id from database {createdCart.CartId}");
      //Act (retrieve) and Assert
      Assert.AreEqual(createdCart.CartId, data.RetrieveCart(cart.CartId).CartId);
    }

    //[TestMethod]
    //public void CanRetrieveCartViaDbContextUsingCartId() {
    //  //Arrange
    //  var cart = NewCart.CreateCartFromProductSelection(_theUri, null, 1, 1, 9.99m);
    //  var data = new WebSiteOrderData(_context, _refContext);
    //  var createdCartId = data.StoreCartWithInitialProduct(cart).CartId;
    //  Debug.WriteLine($"Stored Cart Id from database {createdCartId}");
    //  //Act (retrieve) and Assert
    //  Assert.AreEqual(createdCartId, _context.Carts.FirstOrDefault(c => c.CartId == createdCartId).CartId);
    //}

    [TestMethod]
    public void ProductsHaveValuesWhenReturnedFromRepo()
    {
      var data = new WebSiteOrderData(_context, _refContext);
      var productList = data.GetProductsWithCategoryForShopping();
      Assert.AreNotEqual("", productList[0].Name);
    }

    private void WriteLog()
    {
      Debug.WriteLine(_log);
    }

    private void SetupLogging()
    {
      _context.Database.Log = BuildLogString;
    }

    private void BuildLogString(string message)
    {
      _logBuilder.Append(message);
      _log = _logBuilder.ToString();
    }
  }
}