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

        protected override void OnConfiguring(DbContextOptionsBuilder opts)
            => opts.UseSqlite("Data Source=vendor.db");
    }
}