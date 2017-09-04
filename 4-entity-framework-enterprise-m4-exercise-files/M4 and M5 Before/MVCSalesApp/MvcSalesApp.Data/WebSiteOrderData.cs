using MvcSalesApp.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace MvcSalesApp.Data
{
  public class WebSiteOrderData
  {
    private readonly OrderSystemContext _context;

    public WebSiteOrderData(OrderSystemContext context) {
      _context = context;
    }

    public List<Product> GetProductsWithCategoryForShopping() {
      return _context.Products.AsNoTracking()
          .Include(p => p.Category).Where(p => p.IsAvailable)
          .ToList();
    }

    public RevisitedCart StoreCartWithInitialProduct(NewCart newCart) {
      if (newCart.CartItems.Count != 1) return null;
      CheckForExistingCustomer(newCart);
      _context.Carts.Add(newCart);
      _context.SaveChanges();
      var cart= RevisitedCart.CreateWithItems(newCart.CartId, newCart.CartItems);
      cart.SetCookieData(newCart.CartCookie,newCart.Expires);
      return cart;
    }

    private void CheckForExistingCustomer(NewCart newCart) {
      if (newCart.CustomerCookie != null) {
        var customerId = _context.Customers.AsNoTracking()
          .Where(c => c.CustomerCookie == newCart.CustomerCookie)
          .Select(c => c.CustomerId).FirstOrDefault();
        if (customerId > 0) {
          newCart.CustomerFound(customerId);
        }
      }
    }

    public RevisitedCart RetrieveCart(int cartId)
    {
      var cart= _context.Carts.AsNoTracking().Where(c=>c.CartId==cartId).
        Select(c => new {c.CartId, c.CartItems}).SingleOrDefault();
      if (cart != null) return RevisitedCart.CreateWithItems(cart.CartId, cart.CartItems);
      return RevisitedCart.Create(cartId);
    }

    public RevisitedCart RetrieveCart(string cartCookie)
    {
      var cart= _context.Carts.AsNoTracking().Where(c => c.CartCookie == cartCookie).
       Select(c => new { c.CartId, c.CartItems }).SingleOrDefault();
      if (cart != null) return RevisitedCart.CreateWithItems(cart.CartId, cart.CartItems);
      return null;
    }

    public void StoreNewCartItem(CartItem item) {
      //item should be valid before we get here but one last check
      if (item.CartId == 0) throw new InvalidDataException("Cart Item is not associated with a cart", new InvalidDataException("CartId is 0"));
      _context.CartItems.Add(item);
      _context.SaveChanges();
    }
  }
}