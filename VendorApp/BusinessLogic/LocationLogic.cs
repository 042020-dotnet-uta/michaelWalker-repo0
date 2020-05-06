using System.Collections.Generic;

using VendorApp.DataAccess;
using VendorApp.Model;

namespace VendorApp.BusinessLogic
{
  // TODO add docs
  public class LocationLogic
  {
    // TODO implement the following
    // get, add, update, set customer

    /// <summary>
    /// Retrieves data of a Location specified by it's name using the DataAccess functionality
    /// </summary>
    /// <param name="locationName">Name of the desired location to retrieve</param>
    /// <returns>The data of the desired Location</returns>
    public Location GetLocationByName(string locationName)
    {
      Location location;

      using (var ctx = new VendorContext())
      {
        LocationService locationService = new LocationService(ctx);
        location = locationService.FindByName(locationName);
      }

      return location;
    }

    /// <summary>
    /// Returns a list of all known locations
    /// </summary>
    /// <returns></returns>
    public List<Location> GetAllLocations()
    {
      using (var ctx = new VendorContext())
      {
        LocationService locationService = new LocationService(ctx);
        return locationService.FindAll();
      }
    }

    /// <summary>
    /// Retreives a list of currently registered locations and format's the list into a Packet Text
    /// response.
    /// </summary>
    /// <returns>A Packet with a Text response of the list of locations</returns>
    public Packet GetAllLocationsPacket()
    {
      Packet message = new Packet { Text = "List of Locations:\n\n", Status = PacketStatus.Pass };
      List<Location> locations;

      using (var ctx = new VendorContext())
      {
        LocationService locationService = new LocationService(ctx);
        locations = (List<Location>)locationService.FindAll();
      }

      // Format a list of each location for Message
      foreach (var location in locations)
      {
        message.Text += $"{location.Name}\n";
      }

      return message;
    }

    /// <summary>
    /// Specify a the amount to increase the specified Location's inventory by.
    /// </summary>
    /// <param name="locationName">The location's name that will be used to
    /// retrieve the data from the DB</param>
    /// <param name="productName">The product's name that will be used to
    /// retrieve the data from the DB</param>
    /// <param name="addedAmount">Amount of products to increment to the
    /// Locations inventory</param>
    /// <returns>
    /// A Message object that will state whether or not the restock was
    /// processed succsesfully.
    /// </returns>
    public Packet RestockInventory(string locationName, string productName, int addedAmount)
    {
      // Check for scenarios where issues may occur
      if (addedAmount <= 0)
      {
        return new Packet
        {
          Text = "Please enter a postive number to increase the inventory by.",
          Status = PacketStatus.Invalid
        };
      }
      else if (locationName.Length == 0)
      {
        return new Packet
        {
          Text = "Please enter a valid location name.",
          Status = PacketStatus.Invalid
        };
      }
      else if (productName.Length == 0)
      {
        return new Packet
        {
          Text = "Please enter a valid product name.",
          Status = PacketStatus.Invalid
        };
      }

      const int MAX_INCREMENT = 100;

      Location location = GetLocationByName(locationName);

      ProductLogic productLogic = new ProductLogic();
      Product product = productLogic.FindProductByName(productName);

      ProductInventoryLogic pIL = new ProductInventoryLogic();
      ProductInventory productInventory;

      if (addedAmount > MAX_INCREMENT)
      {
        return new Packet
        {
          Text = $"The max amount of products that can be given to a store at a time is {MAX_INCREMENT}.",
          Status = PacketStatus.Invalid
        };
      }
      else if (location == null)
      {
        return new Packet
        {
          Text = "The Location you've entered does not exist in our system.",
          Status = PacketStatus.Invalid
        };
      }
      else if (product == null)
      {
        return new Packet
        {
          Text = "The Product you've entered does not exist in our system",
          Status = PacketStatus.Invalid
        };
      }

      // All validation has passed, we're now set to restock
      productInventory = pIL.GetProductInventory(location, product);
      int newAmount = productInventory.Quanitity + addedAmount;
      pIL.UpdateInventory(location, product, newAmount);

      return new Packet
      {
        Text = $"Inventory Restocked.  {location.Name}'s quantity of {product.Name} is now {newAmount}",
        Status = PacketStatus.Pass
      };
    }

    // TODO add docs
    public Packet GetLocationInventoryByNamePacket(string locationName)
    {
      ProductInventoryLogic pIL = new ProductInventoryLogic();

      return pIL.GetInventoryByLocationNamePacket(locationName);
    }



    /// <summary>
    /// Retrieves a list of
    /// </summary>
    /// <param name="locationName">Name of the location</param>
    /// <returns>A p</returns>
    public Packet GetLocationOrderHistoryPacket(string locationName)
    {
      // check if location exists
      Location location = GetLocationByName(locationName);
      if (location == null)
      {
        return new Packet
        {
          Text = $"The location {locationName} is not a registered location in our system",
          Status = PacketStatus.Invalid
        };
      }
      // find order history
      OrderLogic orderLogic = new OrderLogic();
      List<Order> locationOrderHistory = orderLogic.FindOrdersByLocationName(locationName);

      if (locationOrderHistory.Count == 0)  // Account Location having no orders
      {
        return new Packet
        {
          Text = "No orders were found for this location",
          Status = PacketStatus.Pass
        };
      }

      // create response

      // Format list to string
      string orderListStr = $"Order history for {locationName}:\n";

      foreach (var order in locationOrderHistory)
      {
        orderListStr += $"Purchaser: {order.Customer.Username},  Product: {order.ProductName},  Amount Purchased: {order.QuantitySold}, Purchase Date: {order.CreatedDate}";
      }

      // Respond with results
      return new Packet
      {
        Text = orderListStr,
        Status = PacketStatus.Pass
      };
    }

    // /// <summary>
    // /// Fetches the inventory of the specified location
    // /// </summary>
    // /// <param name="locationName">Location's name used to retrive inventory</param>
    // /// <returns>A Packet that contains a list of the inventory the location has</returns>
    // public Packet<ProductInventory> RetrieveInventoryPacket(string locationName)
    // {
    //   List<ProductInventory>
    // }

    // TODO Return a list of all customer's names

    // TODO List a specified Location's Inventory

    // TODO Remove Customer

    // TODO Update Customer's Name

    // TODO Write method for validating names
  }
}