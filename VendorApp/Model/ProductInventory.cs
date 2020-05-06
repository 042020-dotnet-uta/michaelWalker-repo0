using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendorApp.Model
{
  /// <summary>
  /// A locations inventory of a particular product
  /// </summary>
  public class ProductInventory
  {
    /// <summary>
    /// Unique identifier
    /// </summary>
    /// <value></value>
    public int ProductInventoryId { get; set; }
    /// <summary>
    /// The prodcut's current location
    /// </summary>
    /// <value></value>
    [Required]
    public Location ProductLocation { get; set; }
    /// <summary>
    /// The product at the specified location
    /// </summary>
    /// <value></value>
    [Required]
    public Product Product { get; set; }
    /// <summary>
    /// The amount of a specific product at the location
    /// </summary>
    /// <value></value>
    [Required]
    public int Quanitity { get; set; }
  }
}