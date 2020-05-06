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
    // TODO add docs
    public int OrderId { get; set; }
    // TODO add docs
    [Required]
    public Customer Customer { get; set; }
    // TODO add docs
    [Required]
    public string ProductName { get; set; }
    // TODO add docs
    [Required]
    public string LocationName { get; set; }

    // TODO add docs
    [Required]
    public int QuantitySold { get; set; }

    // TODO add docs
    // Assign get date within DBContext OnModelCreating
    [Required]
    public DateTime CreatedDate { get; set; }

    public Order()
    {
      // Set the time the order was made on creation
      this.CreatedDate = DateTime.UtcNow;
    }
  }
}