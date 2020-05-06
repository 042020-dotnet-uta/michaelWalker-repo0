using System.Collections.Generic;

using VendorApp.DataAccess;
using VendorApp.Model;

namespace VendorApp.BusinessLogic
{
  // TODO add docs
  public class OrderLogic
  {
    /// <summary>
    /// Stores a record of an order a customer has just purchased.  Saves information on 
    /// what location the order was made from and the product that was purchased
    /// </summary>
    /// <param name="customer">Customer that is being registered as the purchaser</param>
    /// <param name="locationName">The name of the location the order was taken from</param>
    /// <param name="productName">The name of the product that was purchased</param>
    /// <returns>The Order record that was just saved</returns>
    public Order RegisterOrder(Customer customer, string locationName, string productName, int quantitySold)
    {
      Order newOrder;


      using (var ctx = new VendorContext())
      {
        OrderService orderService = new OrderService(ctx);
        newOrder = orderService.Create(customer, locationName, productName, quantitySold);
      }

      return newOrder;
    }

    /// <summary>
    /// Retrieves a list of Orders that a customer has made
    /// </summary>
    /// <param name="username">Name of the Customer the order was made from</param>
    /// <returns>A list of Orders made from a specifed Customer</returns>
    public List<Order> FindOrdersByCustomerName(string username)
    {
      List<Order> customerOrders;

      using (var ctx = new VendorContext())
      {
        OrderService orderService = new OrderService(ctx);
        customerOrders = orderService.FindOrdersByCustomersName(username);
      }

      return customerOrders;
    }

    /// <summary>
    /// Retrieves a list of Orders that a customer has made
    /// </summary>
    /// <param name="username">Name of the Customer the order was made from</param>
    /// <returns>A list of Orders made from a specifed Customer</returns>
    public Packet FindOrdersByCustomerNamePacket(string username)
    {
      // TODO fail case where there are no orders

      List<Order> customerOrders = FindOrdersByCustomerName(username);
      string strListOfOrders = $"List of {username}'s orders:\n";

      foreach (var order in customerOrders)
      {
        strListOfOrders += $"Product: {order.ProductName}, Amount Purchased: {order.QuantitySold}, Purchased From: {order.LocationName}, Purchased On: {order.CreatedDate}\n";
      }

      return new Packet
      {
        Text = strListOfOrders,
        Status = PacketStatus.Pass
      };
    }

    /// <summary>
    /// Retreives a list of Orders made from a specifed location
    /// </summary>
    /// <param name="locationName">Name of the location the order was made from</param>
    /// <returns>A list of Orders made from a specifed location</returns>
    public List<Order> FindOrdersByLocationName(string locationName)
    {
      List<Order> customerOrders;

      using (var ctx = new VendorContext())
      {
        OrderService orderService = new OrderService(ctx);
        customerOrders = orderService.FindOrdersByLocationName(locationName);
      }

      return customerOrders;
    }


    /// <summary>
    /// Retrieves a list of Orders that a customer has made
    /// </summary>
    /// <param name="username">Name of the Customer the order was made from</param>
    /// <returns>A Packet with the list of orders made from the specified Customer</returns>
    public Packet<Order> RetrieveOrdersByCustomerNameFromPacket(string username)
    {
      List<Order> customerOrders;

      using (var ctx = new VendorContext())
      {
        OrderService orderService = new OrderService(ctx);
        customerOrders = orderService.FindOrdersByCustomersName(username);
      }

      return new Packet<Order>
      {
        Text = "Customer Orders retreived.",
        Status = PacketStatus.Pass,
        DataList = customerOrders
      };
    }

    /// <summary>
    /// Retreives a list of Orders made from a specifed location
    /// </summary>
    /// <param name="locationName">Name of the location the order was made from</param>
    /// <returns>A Packet with the list of orders made from the specified location</returns>
    public Packet<Order> RetrieveOrdersByLocationNameFromPacket(string locationName)
    {
      List<Order> locationOrders;

      using (var ctx = new VendorContext())
      {
        OrderService orderService = new OrderService(ctx);
        locationOrders = orderService.FindOrdersByLocationName(locationName);
      }

      return new Packet<Order>
      {
        Text = "Location Orders retreived.",
        Status = PacketStatus.Pass,
        DataList = locationOrders
      }; ;
    }
  }
}