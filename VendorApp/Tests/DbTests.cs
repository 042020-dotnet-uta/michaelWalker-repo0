using Xunit;

using System.Linq;
using Microsoft.EntityFrameworkCore;

using VendorApp.Model;
using VendorApp.DataAccess;

namespace VendorApp.Tests
{
  public class DbTests
  {
    /// <summary>
    /// It will be able to add a customer to the Customer table
    /// </summary>
    [Fact]
    public void AddCustomerTest()
    {
      string customerFirstName = "John";

      var options = new DbContextOptionsBuilder<VendorContext>()
        .UseInMemoryDatabase(databaseName: "TestDB")
        .Options;

      // Save something to imdb
      using (var ctx = new VendorContext(options))
      {
        ctx.Customers.Add(new Customer { Username = customerFirstName, Email = "Doe" });
        ctx.SaveChanges();
      }

      using (var ctx = new VendorContext(options))
      {
        CustomerService customerService = new CustomerService(ctx);
        var result = customerService.FindByUsername(customerFirstName);
        Assert.Equal(customerFirstName, result.Username);
      }

    }
    /// <summary>
    /// It will be able to add a Product to the Customer table
    /// </summary>
    [Fact]
    public void AddProductTest()
    {
      string productName = "MyProduct";
      string productCatagory = "ACatagory";

      var options = new DbContextOptionsBuilder<VendorContext>()
        .UseInMemoryDatabase(databaseName: "TestDB")
        .Options;

      // Save something to imdb
      using (var ctx = new VendorContext(options))
      {
        // ctx.Customers.Add(new Customer { Username = customerFirstName, Email = "Doe" });
        // ctx.Products.Add(new Product { Name = productName, CatagoryType = productCatagory });
        ProductService productService = new ProductService(ctx);
        productService.Create(productName, productCatagory);
      }

      using (var ctx = new VendorContext(options))
      {
        Product product = ctx.Products.FirstOrDefault(p => p.Name == productName && p.CatagoryType == productCatagory);
        Assert.NotNull(product);
      }

    }
    /// <summary>
    /// It will be able to add a location to the Customer table
    /// </summary>
    [Fact]
    public void AddLocationTest()
    {
      string locationName = "NewLocation";

      var options = new DbContextOptionsBuilder<VendorContext>()
        .UseInMemoryDatabase(databaseName: "TestDB")
        .Options;

      // Save something to imdb
      using (var ctx = new VendorContext(options))
      {
        LocationService locationService = new LocationService(ctx);
        locationService.Create(locationName);
      }

      using (var ctx = new VendorContext(options))
      {
        Location location = ctx.Locations.FirstOrDefault(l => l.Name == locationName);
        Assert.NotNull(location);
      }

    }
  }
}