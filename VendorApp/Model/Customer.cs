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
    /// <summary>
    /// Customer's unique ID
    /// </summary>
    /// <value></value>
    public int CustomerId { get; set; }
    /// <summary>
    /// Customer's registered username
    /// </summary>
    /// <value></value>
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }
    /// <summary>
    /// Customer's registered email
    /// </summary>
    /// <value></value>
    [Required]
    [MaxLength(50)]
    public string Email { get; set; }
    /// <summary>
    /// A list of the customer's order history
    /// </summary>
    /// <value></value>
    public ICollection<Order> OrderHistory { get; set; }
  }
}