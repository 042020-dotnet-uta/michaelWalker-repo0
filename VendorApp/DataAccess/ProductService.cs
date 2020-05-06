using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using VendorApp.Model;

namespace VendorApp.DataAccess
{
  /// <summary>
  /// Responsible for handling bussiness logic for the Product model
  /// as well has handling data from the DB.
  /// </summary>
  public class ProductService
  {
    /// <summary>
    /// Context for handling data from DB
    /// </summary>
    private VendorContext ctx;

    public ProductService(VendorContext ctx)
    {
      this.ctx = ctx;
    }

    // * Handling data

    /// <summary>
    /// Add a new product to the database
    /// </summary>
    /// <param name="name">Product's name</param>
    /// <param name="catagory">Product's catagory</param>
    public void Create(string name, string catagory)
    {
      Product p = new Product { Name = name, CatagoryType = catagory };

      ctx.Add(p);
      ctx.SaveChanges();
    }



    /// <summary>
    /// Retrieve Product from the DB by it's unique ID
    /// </summary>
    /// <param name="id">Product's ID</param>
    /// <returns>The produdct entity with the matching id</returns>
    public Product FindById(int id)
    {
      // TODO: AddIn
      // Find Case where Product is null
      return ctx.Products.FirstOrDefault(p => p.ProductId == id);

      
    }

    /// <summary>
    /// Retrieves all records of prodcut entities and stores them into a list
    /// </summary>
    /// <returns>List of product entities</returns>
    public List<Product> FindAll()
    {
      return ctx.Products.ToList();
    }

    // TODO: add docs
    public Product FindByName(string name)
    {
      // TODO: AddIn
      // Find Case where Product is null
      return ctx.Products.FirstOrDefault(p => p.Name == name);
    }

    // Update Product
    public void Update(Product modProd)
    {
      Product p = ctx.Products.FirstOrDefault(p => p.ProductId == modProd.ProductId);

      if (p != null)
      {
        // Apply all the properties from the updated Product to the Product Entity
        p.Name = modProd.Name;
        p.CatagoryType = modProd.CatagoryType;

        ctx.SaveChanges();
      }
    }

    // TODO: add docs
    // Delete Product 
    public void Remove(Product p)
    {
      // TODO: run condition of product doesn't exist
      ctx.Products.Remove(p);
      ctx.SaveChanges();
    }

    // TODO: add docs
    public void RemoveById(int id)
    {
      Remove(FindById(id));
    }

    // TODO: add docs
    public void RemoveByName(string name)
    {
      Remove(FindByName(name));
    }

    // * Business Logic
  }
}