using Microsoft.EntityFrameworkCore;

namespace VendorApp.Model
{
  /// <summary>
  /// Context class for vendor to handle data to and from the database
  /// </summary>
  public class VendorContext : DbContext
  {

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ProductInventory> ProductInventories { get; set; }

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

      // * Product

      // * ProductInventory

      // * Order



      

      

      // modelBuilder.Entity<ProductInventory>(ent =>
      // {
      //   // Many ProductInventory belong to one Location
      //   ent.HasOne(pI => pI.ProductLocation).WithMany(l => l.ProductInventoryRecords);

      //   // ProductInventory is aware of their assigned Product
      //   ent.HasOne(pI => pI.Product);
      // });
    }
  }
}