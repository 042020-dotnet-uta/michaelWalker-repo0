using System.Collections.Generic;

using VendorApp.Model;
using VendorApp.DataAccess;

namespace VendorApp.BusinessLogic
{
  public class ProductInventoryLogic
  {
    /// <summary>
    /// Retrieves the inventory of a Product from a certain Location
    /// </summary>
    /// <param name="location">The Location used to find the inventory</param>
    /// <param name="product">The Product used to find the inventory</param>
    /// <returns></returns>
    public ProductInventory GetProductInventory(Location location, Product product)
    {
      ProductInventory productInventory;
      using (var ctx = new VendorContext())
      {
        ProductInventorieService pIS = new ProductInventorieService(ctx);
        productInventory = pIS.GetInventory(location, product);
      }

      return productInventory;
    }

    /// <summary>
    /// Fetches the inventory of the specified location
    /// </summary>
    /// <param name="locationName">Location's name used to retrive inventory</param>
    /// <returns>List of a location's inventory</returns>
    public List<ProductInventory> RetrieveInventoryByLocationName(string locationName)
    {
      List<ProductInventory> locationInventory;
      using (var ctx = new VendorContext())
      {
        ProductInventorieService pIS = new ProductInventorieService(ctx);
        locationInventory = pIS.FindProductInventoryByLocationName(locationName);
      }

      return locationInventory;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="locationName"></param>
    /// <returns></returns>
    public Packet GetInventoryByLocationNamePacket(string locationName)
    {
      List<ProductInventory> locationInventory = RetrieveInventoryByLocationName(locationName);
      string inventoryListStr = $"{locationName}'s inventory:\n";

      foreach (var inventoryRecord in locationInventory)
      {
        inventoryListStr += $"Product: {inventoryRecord.Product.Name}, Quantity: {inventoryRecord.Quanitity}\n";
      }

      return new Packet
      {
        Text = inventoryListStr,
        Status = PacketStatus.Pass
      };
    }

    /// <summary>
    /// Fetches the inventory of the specified location
    /// </summary>
    /// <param name="locationName">Location's name used to retrive inventory</param>
    /// <returns>A Packet that contains a list of the inventory the location has</returns>
    public Packet<ProductInventory> RetrieveInventoryPacketByLocationName(string locationName)
    {
      List<ProductInventory> locationInventory;
      string locationInventoryStrList;
      using (var ctx = new VendorContext())
      {
        ProductInventorieService pIS = new ProductInventorieService(ctx);
        locationInventory = pIS.FindProductInventoryByLocationName(locationName);
      }

      locationInventoryStrList = $"Inventory for {locationName}:\n";
      foreach (var inventory in locationInventory)
      {
        locationInventoryStrList += $"Product:\n{inventory.Product.Name} - Quantity:\n{inventory.Quanitity}\n\n";
      }

      return new Packet<ProductInventory>
      {
        Text = locationInventoryStrList,
        Status = PacketStatus.Pass
      };
    }

    /// <summary>
    /// Update's the inventory record of the amount of a Location's specific product
    /// </summary>
    /// <param name="location">The Location who's inentory will be updated</param>
    /// <param name="product">The product that will be added/removed from the location</param>
    /// <param name="quantity">The new quantity of the prouct that the current location will carry.</param>
    /// <returns></returns>
    public ProductInventory UpdateInventory(Location location, Product product, int quantity)
    {
      ProductInventory productInventory;
      using (var ctx = new VendorContext())
      {
        ProductInventorieService pIS = new ProductInventorieService(ctx);
        productInventory = pIS.UpdateInventory(location, product, quantity);
      }

      return productInventory;
    }
  }
}