using System;

using VendorApp.DataAccess;
using VendorApp.Model;

namespace VendorApp.DevTools
{
  public class DBSandbox
  {

    internal void RunRounds()
    {
      // RemoveLocationsWithInventory();
      AddLocationsWithInventory();
      // RemoveSomeProducts();

      // using(var ctx = new VendorContext())
      // {
      //   LocationService locationService = new LocationService(ctx);

      //   locationService.RemoveAll();
      //   Console.WriteLine("All locations removed");
      // }
    }
    internal void CreateCustomer(string username)
    {
      Console.WriteLine("Creating Bill");

      using (var ctx = new VendorContext())
      {
        CustomerService cS = new CustomerService(ctx);

        cS.Create(new Customer
        {
          Username = "Bill",
          Email = "Just Bill",
        });
      }

      Console.WriteLine("Bill Has Been Created");
    }

    internal void RemoveCustomer(string username)
    {
      Console.WriteLine($"Removing {username}");

      using (var ctx = new VendorContext())
      {
        CustomerService cS = new CustomerService(ctx);

        cS.RemoveByUsername(username);
      }

      Console.WriteLine($"{username} has been removed");
    }

    private void UpdateFirstCustomer(string oldUsername, string username)
    {
      Console.WriteLine($"Updating Customer {oldUsername}");

      using var ctx = new VendorContext();
      CustomerService cs = new CustomerService(ctx);

      Customer moddedCust = cs.FindByUsername(oldUsername);

      moddedCust.Username = username;

      cs.UpdateCustomer(moddedCust);

      Console.WriteLine($"Updated Customer's Name from {oldUsername} to {username}");
    }

    internal void AddSomeProducts()
    {
      Console.WriteLine("Creating Products");
      using var ctx = new VendorContext();

      ProductService s = new ProductService(ctx);

      s.Create("Stimpak", "Aid");
      s.Create("Buffout", "Aid");
      s.Create("10mm", "Ammo");

      Console.WriteLine("Products Created");
    }

    internal void RemoveSomeProducts()
    {
      Console.WriteLine("Removing Products");

      using var ctx = new VendorContext();
      ProductService s = new ProductService(ctx);

      s.RemoveByName("Stimpak");
      s.RemoveByName("Buffout");
      s.RemoveByName("10mm");

      Console.WriteLine("Products Removed");
    }

    internal void AddLocations()
    {
      Console.WriteLine("Creating Locations");

      using var ctx = new VendorContext();

      LocationService locServ = new LocationService(ctx);

      locServ.Create("The Mojave Wasteland");
      locServ.Create("The Capital Warehouse Store");
      locServ.Create("Diamond City Goods");

      Console.WriteLine("Locations Created");
    }

    internal void RemoveLocations()
    {
      Console.WriteLine("Removing Locations");

      using var ctx = new VendorContext();

      LocationService locServ = new LocationService(ctx);

      locServ.RemoveAll();

      Console.WriteLine("Locations Removed");
    }



    internal void AddLocationsWithInventory()
    {
      AddSomeProducts();
      AddLocations();


      using (var ctx = new VendorContext())
      {
        ProductInventorieService pIServ = new ProductInventorieService(ctx);
        LocationService lServ = new LocationService(ctx);
        ProductService pServ = new ProductService(ctx);

        var prod1 = pServ.FindByName("Stimpak");
        var prod2 = pServ.FindByName("Buffout");
        var prod3 = pServ.FindByName("10mm");

        var loc1 = lServ.FindByName("The Mojave Wasteland");

        lServ.AddProductToInventory(loc1.LocationId, prod1.ProductId, 1);
        lServ.AddProductToInventory(loc1.LocationId, prod2.ProductId, 4);

        var loc2 = lServ.FindByName("The Capital Warehouse Store");
        lServ.AddProductToInventory(loc2.LocationId, prod3.ProductId, 1);
        lServ.AddProductToInventory(loc2.LocationId, prod2.ProductId, 4);
      }
    }

    internal void RemoveLocationsWithInventory()
    {
      RemoveLocations();
      RemoveSomeProducts();
    }


  }
}