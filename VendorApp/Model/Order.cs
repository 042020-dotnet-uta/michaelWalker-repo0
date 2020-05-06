using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendorApp.Model
{
  /// <summary>
  /// The Customer Model
  /// The customers info will be stored when they place an order
  /// </summary>
  public class Order
  {
    /// <summary>
    /// The order's unique ID
    /// </summary>
    /// <value></value>
    public int OrderId { get; set; }
    /// <summary>
    /// The customer that made the order
    /// </summary>
    /// <value></value>
    [Required]
    public Customer Customer { get; set; }
    /// <summary>
    /// The product the customer purchased in the order
    /// </summary>
    /// <value></value>
    [Required]
    public string ProductName { get; set; }
    /// <summary>
    /// The location the customer made the order from
    /// </summary>
    /// <value></value>
    [Required]
    public string LocationName { get; set; }

    /// <summary>
    /// The amount of the product purchased by the customer
    /// </summary>
    /// <value></value>
    [Required]
    public int QuantitySold { get; set; }

    ///
    /// <summary>
    /// The timestamp when the purchase was made
    /// </summary>
    /// <value></value>
    [Required]
    // Assign get date within DBContext OnModelCreating
    public DateTime CreatedDate { get; set; }

    public Order()
    {
      // Set the time the order was made on creation
      this.CreatedDate = DateTime.UtcNow;
    }
  }
}