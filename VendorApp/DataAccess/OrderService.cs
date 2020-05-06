using System.Collections.Generic;
using System.Linq;
using System;

using Microsoft.EntityFrameworkCore;

using VendorApp.Model;

// TODO finish adding todos
namespace VendorApp.DataAccess
{
  /// <summary>
  /// The order service class is the layer that handles all the business logic
  /// and data fetching/manipulation for the Order Model.
  /// </summary>
  public class OrderService
  {
    /// <summary>
    /// Context for managing data from the DB
    /// </summary>
    private VendorContext ctx;

    public OrderService(VendorContext ctx)
    {
      this.ctx = ctx;
    }

    /// <summary>
    /// Add a new order to the database
    /// </summary>
    /// <param name="firstName">Order's first name</param>
    /// <param name="lastName">Order's last name</param>
    public Order Create(Customer _customer, string locationName, string productName, int quantitySold)
    {
      Customer customer = ctx.Customers.Find(_customer.CustomerId);
      Order order = new Order { Customer = customer, LocationName = locationName, ProductName = productName, QuantitySold = quantitySold };
      ctx.Orders.Add(order);
      ctx.SaveChanges();

      return order;
    }

    // TODO: finish documenting
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>The order based on ID or null if no order was found</returns>
    public Order FindById(int id)
    {
      Order orderToFind = ctx.Orders.FirstOrDefault(c => c.OrderId == id);
      if (orderToFind == null)
      {
        // No order was found with that id
        return null;
      }

      return orderToFind;
    }

    /// <summary>
    /// Queries the DB and returns the first order that matches the provided username
    /// </summary>
    /// <param name="firstName">Order's first name</param>
    /// <returns>A order with the matching first name</returns>
    public List<Order> FindOrdersByCustomersName(string usrname)
    {
      return ctx.Orders.Where(o => o.Customer.Username == usrname).ToList();
    }

    /// <summary>
    /// Queries the DB and returns the first order that matches the provided username
    /// </summary>
    /// <param name="firstName">Order's first name</param>
    /// <returns>A order with the matching first name</returns>
    public List<Order> FindOrdersByLocationName(string locationName)
    {
      return ctx.Orders.Include(o => o.Customer).Where(o => o.LocationName == locationName).ToList();
    }


    public void RemoveByOrder(Order c)
    {
      ctx.Orders.Remove(c);
      ctx.SaveChanges();
    }

    // TODO: start docs
    public void RemoveById(int id)
    {
      RemoveByOrder(FindById(id));
    }

  }
}