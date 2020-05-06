using VendorApp.DataAccess;
using VendorApp.Model;

using System.Collections.Generic;

namespace VendorApp.BusinessLogic
{
  // TODO add docs
  public class ProductLogic
  {
    // TODO implement the following
    // get, add, update, set customer

    /// <summary>
    /// Searches for and retrieves a product entity via name.
    /// </summary>
    /// <param name="productName">The name of the desired product to retrieve</param>
    /// <returns>Product with matching name</returns>
    public Product FindProductByName(string productName)
    {
      Product product;

      using (var ctx = new VendorContext())
      {
        ProductService productService = new ProductService(ctx);
        product = productService.FindByName(productName);
      }

      return product;
    }

    /// <summary>
    /// Returns a list of products
    /// </summary>
    /// <returns></returns>
    public List<Product> GetProductList()
    {
      using(var ctx = new VendorContext())
      {
        ProductService productService = new ProductService(ctx);

        return productService.FindAll();
      }
    }

    
    

    // public Packet AddProduct()
    // {

    // }

    // TODO Return a list of all Product's names

    // TODO List a specified Location's Inventory

    // TODO Remove Product

    // TODO Update Product's Name

    // TODO Write method for validating names
  }
}