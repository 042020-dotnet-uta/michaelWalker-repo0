using Microsoft.EntityFrameworkCore;

namespace VendorApp.Model
{
    /// <summary>
    /// Context class for vendor to handle data to and from the database
    /// </summary>
    public class VendorContext : DbContext
    {

        public DbSet<Customer> Customers { get; set; }

        public VendorContext()
        {

        }

        public VendorContext(DbContextOptions<VendorContext> opts) : base(opts) { }

        // Configure db to use local sqlite file in root
        protected override void OnConfiguring(DbContextOptionsBuilder opts)
            => opts.UseSqlite("Data Source=vendor.db");
    }
}