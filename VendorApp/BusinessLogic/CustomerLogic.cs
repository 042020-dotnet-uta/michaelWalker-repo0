using System.Collections.Generic;

using VendorApp.DataAccess;
using VendorApp.Model;

namespace VendorApp.BusinessLogic
{
  // TODO add docs
  public class CustomerLogic
  {
    // TODO implement the following
    // get, add, update, set customer

    /// <summary>
    /// Retrieves Customer from the DB with a specified first name
    /// </summary>
    /// <param name="firstName">The name to search for</param>
    /// <returns>A Customer with the matching firstName or NULL if none is found</returns>
    public Customer FindCustomerByUsername(string username)
    {
      Customer customer;

      using (var ctx = new VendorContext())
      {
        CustomerService cServ = new CustomerService(ctx);
        customer = cServ.FindByUsername(username);
      }

      return customer;
    }

    /// <summary>
    /// Inserts Customer into the DB
    /// </summary>
    /// <param name="username">Customer's username</param>
    /// <param name="email">Customer's email</param>
    /// <returns>A Message specifying the status of the insert</returns>
    public Packet AddCustomer(string username, string email)
    {
      // Validate username and email
      if (username.Length == 0)
      {
        return new Packet{
          Text = "Please enter a valid username",
          Status = PacketStatus.Invalid
        };
      }
      else if (username.Length > 10)
      {
        return new Packet{
          Text = "Usernames need to be 10 characters or less",
          Status = PacketStatus.Invalid
        };
      }
      else if (email.Length == 0)
      {
        return new Packet{
          Text = "Please enter a valid email",
          Status = PacketStatus.Invalid
        };
      }
      else if (email.Length > 50)
      {
        return new Packet{
          Text = "Emails need to be 50 characters or less",
          Status = PacketStatus.Invalid
        };
      }

      Customer customer = new Customer { Username = username, Email = email };
      using (var ctx = new VendorContext())
      {
        CustomerService customerService = new CustomerService(ctx);
        customerService.Create(customer);
      }

      return new Packet { Text = $"Customer {customer.Username} created!", Status = PacketStatus.Pass };
    }

    /// <summary>
    /// Retreives a list of all registered users in the DB into a Packet.  Formats a the list of customers 
    /// in the Text response of the packet and also stores it as a list in the DataList property.
    /// </summary>
    /// <returns></returns>
    public Packet<Customer> GetCustomerList()
    {
      Packet<Customer> packet = new Packet<Customer>();

      using var ctx = new VendorContext();
      CustomerService cServ = new CustomerService(ctx);
      List<Customer> customers = cServ.FindAll();

      if (customers.Count == 0)
      {
        packet.Text = "No Customers were found";
        packet.Status = PacketStatus.Pass;
      }
      else
      {
        packet.DataList = customers;

        packet.Text += "List of Customers:\n";
        // Loop through each customer and create a List of their names
        foreach (var customer in customers)
        {
          packet.Text += $"{customer.Username}\n";
        }

        packet.Status = PacketStatus.Pass;
      }

      return packet;
    }

    // TODO: add docs
    public Packet<Customer> GetCustomerDetailsByName(string username)
    {
      Customer customer;
      Packet<Customer> msg = new Packet<Customer> { Text = "", Status = PacketStatus.NULL };


      using (var ctx = new VendorContext())
      {
        CustomerService cServ = new CustomerService(ctx);
        customer = cServ.FindByUsername(username);
      }
      if (customer != null)
      {
        msg.Text = $"Username: {customer.Username} | email: {customer.Email} | # of Orders: {customer.OrderHistory.Count}";
        msg.Status = PacketStatus.Pass;
      }
      else
      {
        msg.Text = "A user wasn't found with that name, please try again";
        msg.Status = PacketStatus.Invalid;
      }

      return msg;
    }

    // TODO: add docs
    public Packet GetCustomerOrdersByUserNamePacket(string username)
    {
      Packet message = new Packet();
      Customer customer = FindCustomerByUsername(username);

      if (customer.OrderHistory.Count == 0)
      {
        List<Order> customerOrders = (List<Order>)customer.OrderHistory;
        message.Text = $"Order history for {customer.Username}:\n\n";

        // Append each order's details to Message in a vertical list
        foreach (var order in customerOrders)
        {
          // TODO: Specify amount purchased in Order model
          message.Text += $"Prodcut: {order.ProductName} Date Purchased: {order.CreatedDate}";
        }

      }
      else
      {
        message.Text = "{} hasn't made any purchases...";
      }
      // Add some bottom padding
      message.Text = "\n\n";
      message.Status = PacketStatus.Pass;

      return message;
    }

    /// <summary>
    /// Registers a customer making a purchase
    /// </summary>
    /// <param name="customerName">
    ///  The name of the Customer making the purchase
    /// </param>
    /// <param name="locationName">
    ///  The name of the location the customer is purchasing from.
    /// </param>
    /// <param name="productName">
    /// The product being purchased
    /// </param>
    /// <param name="quantity">
    ///  The amount of the product being purchased
    /// </param>
    /// <returns>A Packet containing info of the purchase</returns>
    public Packet MakePurchase(string username, string locationName, string productName, int quantity)
    {
      CustomerLogic customerLogic = new CustomerLogic();
      Customer customer;

      LocationLogic locationLogic = new LocationLogic();
      Location location;

      ProductInventoryLogic pIL = new ProductInventoryLogic();
      ProductInventory productInventory;

      ProductLogic productLogic = new ProductLogic();
      Product product;

      OrderLogic orderLogic = new OrderLogic();

      // check if customer Exists
      customer = customerLogic.FindCustomerByUsername(username);
      if (customer == null)
      {
        return new Packet
        {
          Text = "That customer does not exist.",
          Status = PacketStatus.Invalid,
        };
      }

      // check location exists
      location = locationLogic.GetLocationByName(locationName);
      if (location == null)
      {
        return new Packet
        {
          Text = "The location specified was not found.",
          Status = PacketStatus.Invalid,
        };
      }
      // check product exists
      product = productLogic.FindProductByName(productName);
      if (product == null)
      {
        return new Packet
        {
          Text = "The product specified was not found.",
          Status = PacketStatus.Invalid,
        };
      }

      // Check location has product in stock
      productInventory = pIL.GetProductInventory(location, product);
      if (productInventory == null)
      {
        return new Packet
        {
          Text = "This location does not currently carry this product",
          Status = PacketStatus.Invalid
        };
      }

      // Check to see if customer isn't buying too many products
      if (quantity > productInventory.Quanitity)
      {
        return new Packet
        {
          Text = $"This location does not carry the amount of {product.Name} you are trying to purchase",
          Status = PacketStatus.Invalid
        };
      }

      // All's good, being registering purchase

      // Decrease quantity of location by the amount the customer is purchasing
      int quantitySold = productInventory.Quanitity - quantity;
      pIL.UpdateInventory(location, product, quantitySold);

      // Register Order for customer
      orderLogic.RegisterOrder(customer, locationName, productName, quantity);

      return new Packet
      {
        Text = $"Order purchased successfully - Purchaser {username} ordered product {productName} from location {locationName}",
        Status = PacketStatus.Pass
      };
    }

    // TODO Remove Customer

    // TODO Update Customer's Name

    // TODO Have customer make a purchase

    // TODO Write method for validating names
  }
}