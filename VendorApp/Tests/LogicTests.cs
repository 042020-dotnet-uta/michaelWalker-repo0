using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using VendorApp.Model;
using VendorApp.DataAccess;
using VendorApp.BusinessLogic;

namespace VendorApp.Tests
{
  public class LogicTests
  {

    // * ProductInventory Tests
    public void FindProductInInventory()
    {
      // ProductInventorieservice pIS = new
    }


    /// <summary>
    /// It should fail when creating a Customer with too long a name
    /// </summary>
    [Fact]
    public void TooLongCustomerUsernameTest()
    {
      CustomerLogic customerLogic = new CustomerLogic();
      string longUsername = "ThisIsAReallyLongUsername";
      string expectedFailMessage = "Usernames need to be 10 characters or less";
      var response = customerLogic.AddCustomer(longUsername, "e@mail.com");

      Assert.Equal(expectedFailMessage, response.Text);
    }

    /// <summary>
    /// It should fail when creating a Customer with too long an email
    /// </summary>
    [Fact]
    public void TooLongCustomerEmailTest()
    {
      CustomerLogic customerLogic = new CustomerLogic();
      string longEmail = "ThisIsAReallyReallyReallyReallyLongUsername@emailadress.com";
      string expectedFailMessage = "Emails need to be 50 characters or less";
      var response = customerLogic.AddCustomer("aUser", longEmail);

      Assert.Equal(expectedFailMessage, response.Text);
    }

    /// <summary>
    /// It should fail when creating a Customer with an empty name
    /// </summary>
    [Fact]
    public void EmptyCustomerUsernameTest()
    {
      CustomerLogic customerLogic = new CustomerLogic();
      string expectedFailMessage = "Please enter a valid username";
      var response = customerLogic.AddCustomer("", "e@mail.com");

      Assert.Equal(expectedFailMessage, response.Text);
    }

    /// <summary>
    /// It should fail when creating a Customer with an empty email
    /// </summary>
    [Fact]
    public void EmptyCustomerEmailTest()
    {
      CustomerLogic customerLogic = new CustomerLogic();
      string expectedFailMessage = "Please enter a valid email";
      var response = customerLogic.AddCustomer("aUser", "");

      Assert.Equal(expectedFailMessage, response.Text);
    }

    /// <summary>
    /// It will fail when restocking an inventory with a negative amount
    /// </summary>
    [Fact]
    public void RestockWithNegativeInventory()
    {
      LocationLogic locationLogic = new LocationLogic();
      var response = locationLogic.RestockInventory("location", "product", -10);
      string expectedFailMessage = "Please enter a postive number to increase the inventory by.";
      Assert.Equal(response.Text, expectedFailMessage);
    }
    /// <summary>
    /// It will fail when the location name entered is invalid
    /// </summary>
    [Fact]
    public void RestockWithInvalidLocation()
    {
      LocationLogic locationLogic = new LocationLogic();
      var response = locationLogic.RestockInventory("", "product", 10);
      string expectedFailMessage = "Please enter a valid location name.";
      Assert.Equal(response.Text, expectedFailMessage);
    }
    /// <summary>
    /// It will fail when the product name entered is invalid
    /// </summary>
    [Fact]
    public void RestockWithInvalidProduct()
    {
      LocationLogic locationLogic = new LocationLogic();
      var response = locationLogic.RestockInventory("location", "", 10);
      string expectedFailMessage = "Please enter a valid product name.";
      Assert.Equal(response.Text, expectedFailMessage);
    }
  }
}