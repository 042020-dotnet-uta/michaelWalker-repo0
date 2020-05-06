using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using VendorApp.Model;

namespace VendorApp.DataAccess
{
  /// <summary>
  /// The customer service class is the layer that handles all the business logic
  /// and data fetching/manipulation for the Customer Model.
  /// </summary>
  public class CustomerService
  {
    /// <summary>
    /// Context for managing data from the DB
    /// </summary>
    private VendorContext ctx;

    public CustomerService(VendorContext ctx)
    {
      this.ctx = ctx;
    }

    /// <summary>
    /// Add a new customer to the database
    /// </summary>
    /// <param name="username">Customer's username</param>
    /// <param name="email">Customer's email</param>
    public void Create(Customer customer)
    {
      ctx.Customers.Add(customer);

      // ! Catch error if user couldnt be saved
      try
      {
        ctx.SaveChanges();
      }
      catch (DbUpdateException e)
      {
        Console.WriteLine("An issue occured when trying to save a customer into the DB");
        Console.WriteLine(e.InnerException.Message);
      }
    }

    /// <summary>
    /// Retrieve customer by id
    /// </summary>
    /// <param name="id">The Customer's unique identifier</param>
    /// <returns>The customer based on ID or null if no customer was found</returns>
    public Customer FindById(int id)
    {
      if (!ctx.Customers.Any(c => c.CustomerId == id))
      {
        // No customer was found with that id
        return null;
      }

      return ctx.Customers.AsNoTracking().Include(c => c.OrderHistory).FirstOrDefault(c => c.CustomerId == id);
    }

    /// <summary>
    /// Returns a list of all the Customers found within the DB
    /// </summary>
    /// <returns>List of Customers</returns>
    public List<Customer> FindAll()
    {
      using (var ctx = new VendorContext())
      {
        return ctx.Customers.ToList();
      }
    }

    /// <summary>
    /// Queries the DB and returns the first customer that matches the provided username
    /// </summary>
    /// <param name="username">Customer's first name</param>
    /// <returns>A customer with the matching first name</returns>
    public Customer FindByUsername(string username)
    {

      var customers = from cust in ctx.Customers
                      where cust.Username == username
                      select cust;

      return customers.AsNoTracking().Include(c => c.OrderHistory).FirstOrDefault();
    }

    /// <summary>
    /// Updates an existing Customer's attributes to the DB
    /// </summary>
    /// <param name="moddedCust"></param>
    public void UpdateCustomer(Customer moddedCust)
    {
      // Get Customer by id
      Customer cust = ctx.Customers.FirstOrDefault(c => c.CustomerId == moddedCust.CustomerId);

      if (moddedCust != null)
      {
        // Apply all properties from the modded Customer to the entity Customer
        cust.Username = moddedCust.Username;
        cust.Email = moddedCust.Email;

        ctx.SaveChanges();
      }
    }

    /// <summary>
    /// Removes a customer from the DB by using a reference the specified 
    /// Customer retrieved from the DBContext
    /// </summary>
    /// <param name="c">The customer entity</param>
    public void RemoveByCustomer(Customer c)
    {
      ctx.Customers.Remove(c);
      ctx.SaveChanges();
    }

    /// <summary>
    /// Remove's a customer with a specified id
    /// </summary>
    /// <param name="id">Customer's unique ID</param>
    public void RemoveById(int id)
    {
      RemoveByCustomer(FindById(id));
    }

    /// <summary>
    /// Remove's a customer with a specified id
    /// </summary>
    /// <param name="username">Customer's ID</param>
    public void RemoveByUsername(string username)
    {
      RemoveByCustomer(FindByUsername(username));
    }

  }
}