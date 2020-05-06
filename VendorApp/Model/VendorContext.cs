using Microsoft.EntityFrameworkCore;

namespace VendorApp.Model
{
  /// <summary>
  /// Context class for vendor to handle data to and from the database
  /// </summary>
  public class VendorContext : DbContext
  {

    
    /// <summary>
    /// The Customer's DB set.  Used to retrieve and manipulate data from the 
    /// DB relative to the Customer model.
    /// </summary>
    public DbSet<Customer> Customers { get; set; }
    /// <summary>
    /// The Locatiion's DB set.  Used to retrieve and manipulate data from the 
    /// DB relative to the Locatiion model.
    /// </summary>
    public DbSet<Location> Locations { get; set; }
    /// <summary>
    /// The Product's DB set.  Used to retrieve and manipulate data from the 
    /// DB relative to the Product model.
    /// </summary>
    public DbSet<Product> Products { get; set; }
    /// <summary>
    /// The Order's DB set.  Used to retrieve and manipulate data from the 
    /// DB relative to the Order model.
    /// </summary>
    public DbSet<Order> Orders { get; set; }
    /// <summary>
    /// The LocationInventory's DB set.  Used to retrieve and manipulate data from the 
    /// DB relative to the LocationInventory model.
    /// </summary>
    public DbSet<ProductInventory> LocationInventory { get; set; }

    public VendorContext()
    {

    }

    public VendorContext(DbContextOptions<VendorContext> opts) : base(opts) { }

    // Configure db to use local sqlite file in root
    protected override void OnConfiguring(DbContextOptionsBuilder opts)
    {
      if (!opts.IsConfigured)
      {
        opts.UseSqlite("Data Source=vendor.db");
      }
    }

    // Configure object modeling when DB is created
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // * Customer
      // Assign one2many relationship from Customer to Order
      modelBuilder.Entity<Customer>()
                    .HasMany(c => c.OrderHistory)
                    .WithOne(o => o.Customer);


      // * Location
      // Assign one2many relationship from Location to ProductInventory
      modelBuilder.Entity<Location>()
                    .HasMany(l => l.ProductInventoryRecords)
                    .WithOne(pI => pI.ProductLocation);
    }
  }
}