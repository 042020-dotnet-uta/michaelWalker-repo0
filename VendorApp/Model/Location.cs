using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendorApp.Model
{
  /// <summary>
  /// The definitive location of where the goods are stored.
  /// Customers will be able to purchase products from one of the locations.
  /// Each location has their own unique set and quanity of inventory(products).
  /// </summary>
  public class Location
  {
    /// <summary>
    /// Unique Identifier
    /// </summary>
    /// <value></value>
    [Key]
    public int LocationId { get; set; }
    /// <summary>
    /// The name of the location
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    /// <summary>
    /// List of the current location's inventory of it's current products and the
    /// amount of each product it has.
    /// </summary>
    /// 
    /// <remark>One to Many (location to productInventory) specified in OnModelCreating</remark>
    public ICollection<ProductInventory> ProductInventoryRecords { get; set; }

  }
}