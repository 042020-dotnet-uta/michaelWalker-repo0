using System.ComponentModel.DataAnnotations;

namespace VendorApp.Model
{
  /// <summary>
  /// The product that will be stored inside each location
  /// </summary>
  public class Product
  {
    /// <summary>
    /// Unique Identifier
    /// </summary>
    /// <value></value>
    [Key]
    public int ProductId { get; set; }
    /// <summary>
    /// The products Name
    /// </summary>
    /// <value></value>
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    /// <summary>
    /// Defined keyword to group product into a certain genre
    /// </summary>
    /// <value></value>
    [Required]
    [MaxLength(50)]
    public string CatagoryType { get; set; }

  }
}