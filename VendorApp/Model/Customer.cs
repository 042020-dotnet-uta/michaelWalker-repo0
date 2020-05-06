using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendorApp.Model
{
  /// <summary>
  /// The Customer Model
  /// The customers info will be stored when they place an order
  /// </summary>
  public class Customer
  {
    // TODO: add docs
    public int CustomerId { get; set; }
    // TODO: add docs
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }
    // TODO: add docs
    [Required]
    [MaxLength(50)]
    public string Email { get; set; }
    // TODO: add docs
    public ICollection<Order> OrderHistory { get; set; }
  }
}