using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using VendorApp.Model;

namespace VendorApp.DataAccess
{
  /// <summary>
  /// Responsible for handling bussiness logic for the ProductInventory model
  /// as well has handling data from the DB.
  /// </summary>
  public class ProductInventorieService
  {
    /// <summary>
    /// Context for handling data from DB
    /// </summary>
    private VendorContext ctx;

    public ProductInventorieService(VendorContext ctx)
    {
      this.ctx = ctx;
    }

    // * Handling data

    // Create a ProductInventory record
    // TODO: add docs
    public void Create(Location location, Product product, int quantity = 0)
    {

      // TODO throw exception on negative quantity

      ProductInventory pI = new ProductInventory
      {
        ProductLocation = location,
        Product = product,
        Quanitity = quantity,
      };

      ctx.LocationInventory.Add(pI);
      ctx.SaveChanges();
    }

    /// <summary>
    /// Retrieves a specified Location's Inventory
    /// </summary>
    /// <param name="location">The Location related to the inventory</param>
    /// <param name="product">The Product that is registered to the Location</param>
    /// <returns>The ProductInventory which includes the Location and Product
    /// it's related to.</returns>
    public ProductInventory GetInventory(Location location, Product product)
    {
      return ctx.LocationInventory.AsNoTracking().Include(pI => pI.ProductLocation).Include(pI => pI.Product)
          .FirstOrDefault(pI => pI.ProductLocation.LocationId == location.LocationId && pI.Product.ProductId == product.ProductId);
    }

    /// <summary>
    /// Retrieves Location's inventory based off of Location's name
    /// </summary>
    /// <param name="locationName">Name used to fileter query results</param>
    /// <returns>List of Location's inventory</returns>
    public List<ProductInventory> FindProductInventoryByLocationName(string locationName) => 
        ctx.LocationInventory.AsNoTracking().Include(pI => pI.ProductLocation).Include(pI => pI.Product)
        .Where(pI => pI.ProductLocation.Name == locationName).ToList();

    /// <summary>
    /// Updates the quanity of products the specified location has
    /// </summary>
    /// <param name="location">The location who's inventory will be updated</param>
    /// <param name="product">The product who's qauntity will be changed</param>
    /// <returns>An instance of ProductInventory with the resulting changes after the update.</returns>
    public ProductInventory UpdateInventory(Location location, Product product, int quantity)
    {
      ProductInventory productInventory = ctx.LocationInventory.Include(pI => pI.ProductLocation).Include(pI => pI.Product)
          .FirstOrDefault(pI => pI.ProductLocation.LocationId == location.LocationId && pI.Product.ProductId == product.ProductId);
      productInventory.Quanitity = quantity;
      ctx.SaveChanges();

      return productInventory;
    }

    // Delete ProductInventory record

    // TODO: add docs


    // Update the quantity of the product

    // Find ProductInventory data

    /// <summary>
    /// Find the Location the Product belongs to.
    /// </summary>
    /// <param name="locationId">Unique id of Location</param>
    /// <returns>The Product that belongs to the Location's Inventory</returns>
    public ProductInventory FindByLocationId(int locationId)
    {
      return ctx.LocationInventory.Where(pI => locationId == pI.ProductLocation.LocationId).First();
    }
  }
}