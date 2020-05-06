using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VendorApp.Model;

namespace VendorApp.DataAccess
{
  /// <summary>
  /// Responsible for handling bussiness logic for the location model
  /// as well has handling data from the DB.
  /// </summary>
  public class LocationService
  {
    /// <summary>
    /// Context for handling data from DB
    /// </summary>
    private VendorContext ctx;

    public LocationService(VendorContext ctx)
    {
      this.ctx = ctx;
    }

    // Create a location
    /// <summary>
    /// Create a new location with a provided name.
    /// The location will have no inventory starting out.
    /// </summary>
    /// <param name="name">The name that will be given to Location</param>
    public void Create(string name)
    {
      Location newLoc = new Location { Name = name };
      ctx.Locations.Add(newLoc);
      ctx.SaveChanges();
    }

    /// <summary>
    /// Queries the DB for Location by it's name then removes it from the record.
    /// </summary>
    /// <param name="name">Name used to search for Location</param>
    public void RemoveById(int id)
    {
      Location loc = ctx.Locations.FirstOrDefault(l => l.LocationId == id);
      // Remove InventoryRecords from this Location
      List<ProductInventory> inventoryRecords = ctx.LocationInventory.Where(pI => pI.ProductLocation.LocationId == loc.LocationId).ToList();
      foreach (var record in inventoryRecords)
      {
        ctx.LocationInventory.Remove(record);
      }

      //Remove Location
      ctx.Locations.Remove(loc);
      ctx.SaveChanges();
    }

    /// <summary>
    /// Queries the DB for Location by it's name then removes it from the record.
    /// </summary>
    /// <param name="name">Name used to search for Location</param>
    public void RemoveByName(string name)
    {
      Location loc = ctx.Locations.FirstOrDefault(l => l.Name == name);
      // Remove InventoryRecords from this Location
      List<ProductInventory> inventoryRecords = ctx.LocationInventory.Where(pI => pI.ProductLocation.Name == loc.Name).ToList();
      foreach (var record in inventoryRecords)
      {
        ctx.LocationInventory.Remove(record);
      }

      //Remove Location
      ctx.Locations.Remove(loc);
      ctx.SaveChanges();
    }

    // TODO: add docs
    public void RemoveAll()
    {
      // Fetch ALL locations
      List<Location> locations = ctx.Locations.ToList();
      ctx.Locations.RemoveRange(locations);
      //Remove Location

      ctx.SaveChanges();
    }

    // Find location by id, name
    /// <summary>
    /// Fetches Location from DB that matches the provided id
    /// </summary>
    /// <param name="id">Locations unique id</param>
    /// <returns>Location returned by DB</returns>
    public Location FindById(int id)
    {
      return ctx.Locations.Where(l => l.LocationId == id).First();
    }
    /// <summary>
    /// Fetches Location from DB that matches the provided name
    /// </summary>
    /// <param name="name">Name to search DB with</param>
    /// <returns></returns>
    public Location FindByName(string name)
    {
      return ctx.Locations.SingleOrDefault(l => l.Name == name);
    }

    /// <summary>
    /// Retrieves all Locations within the DB
    /// </summary>
    /// <returns>A List of type Location</returns>
    public List<Location> FindAll()
    {
      return ctx.Locations.ToList();
    }

    // Add/Restock a Product(s) to this location
    public void AddProductToInventory(int locationId, int productId, int quantity = 1)
    {
      // Fetch the location and the product to add to location
      Product prod = ctx.Products.Find(productId);
      // * Find ProductInventoryRecord
      // Location loc = ctx.Locations.Include(l => l.ProductInventoryRecords).FirstOrDefault(l => l.LocationId == locationId);
      Location loc = ctx.Locations.Find(locationId);

      // Add inventory record for location
      ProductInventorieService pIS = new ProductInventorieService(ctx);
      pIS.Create(loc, prod, quantity);
    }

    // TODO: add docs
    public ProductInventory UpdateLocationInventory(Location location, Product product, int quantity)
    {
      ProductInventory productInventory;

      using(var ctx = new VendorContext())
      {
        ProductInventorieService pIS = new ProductInventorieService(ctx);

        productInventory = pIS.UpdateInventory(location, product, quantity);
      }

      return productInventory;
    }

    // Sell Location's Product(s) to a customer

    /// <summary>
    /// Finds the Inventory for a specific product
    /// </summary>
    /// <param name="productInventoryRecords">A list of records including various products and their quanitity</param>
    /// <param name="productName">The specified product to search for</param>
    /// <returns>The Inventory for the specific product or null if the product was not found</returns>
    public ProductInventory FindProductRecord(ICollection<ProductInventory> productInventoryRecords, string productName)
    {
      if (productInventoryRecords.Any(pIR => pIR.Product.Name == productName))
      {
        return productInventoryRecords.FirstOrDefault(pIR => pIR.Product.Name == productName);
      }

      return null;
    }
  }
}