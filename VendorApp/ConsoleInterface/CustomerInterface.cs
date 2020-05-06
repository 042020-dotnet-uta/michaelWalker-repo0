using System;

using VendorApp.BusinessLogic;

namespace VendorApp.ConsoleInterface
{
  // TODO add docs
  public class CustomerInterface
  {
    /// <summary>
    /// Determines if the CustomerInterface menu should 
    /// continue to display
    /// </summary>
    private static bool isRunning;

    // TODO add docs
    public static void ShowCustomerMenu()
    {
      isRunning = true;
      // TODO: Show options and implement functionality for
      // ✅ 1. List Customers 
      // ✅ 2. Add Customer
      // ✅ 3. Dispaly Customer details
      // ✅ 4. Display Customer order history

      string choice;

      while (isRunning)
      {
        Console.WriteLine(
@"
Customer Menu - Please make a selection:
1. List Customers
2. Add new Customer
3. Display Customer details
4. Display a Customer's order history
5. Have Customer make a purchase
0. Back to main menu
"
        );

        choice = Console.ReadLine();

        switch (choice)
        {
          case "0":
            isRunning = false;
            break;
          case "1":
            DisplayCustomers();
            break;
          case "2":
            AddCustomer();
            break;
          case "3":
            ShowCustomersDetails();
            break;
          case "4":
            DisplayCustomerOrderHistory();
            break;
          case "5":
            PromptForCustomerPurchase();
            break;
          default:
            Console.WriteLine("Invalid selection.  Please try again...\n");
            break;
        }
      }
    }

    /// <summary>
    /// Prompts the user to enter the credentials for creating a new
    /// customer.
    /// </summary>
    private static void AddCustomer()
    {
      bool retry = true; // process will continue to retry if

      string usernameInput;
      string emailInput;
      CustomerLogic customerLogic = new CustomerLogic();

      do
      {
        // Prompt user to enter usrname and email address
        Console.WriteLine("Please enter a username (max length 10):");
        usernameInput = Console.ReadLine();

        Console.WriteLine("Please enter a email (max length 50):");
        emailInput = Console.ReadLine();

        var response = customerLogic.AddCustomer(usernameInput, emailInput);

        Console.WriteLine(response.Text + "\n");
        if (response.Status == PacketStatus.Invalid)
        {
          Console.WriteLine("Please try again...\n");
        }
        else
        {
          retry = false;
        }


      } while (retry);
    }

    /// <summary>
    /// Displays list of customers to the Console Interface
    /// </summary>
    private static void DisplayCustomers()
    {
      CustomerLogic customerLogic = new CustomerLogic();

      var response = customerLogic.GetCustomerList();

      Console.WriteLine(response.Text);

    }

    /// <summary>
    /// Prompts the user to enter the name of the Customer 
    /// they want retrieve details for.
    /// </summary>
    private static void ShowCustomersDetails()
    {
      CustomerLogic customerLogic = new CustomerLogic();
      string usernameInput;
      bool retry; // determine whether this process needs to be retried

      DisplayCustomers(); // Display list for user to select from

      do
      {
        Console.WriteLine("Please enter a Customer's username to fetch their details.");
        usernameInput = Console.ReadLine();
        var response = customerLogic.GetCustomerDetailsByNamePacket(usernameInput);

        if (response.Status == PacketStatus.Invalid)
        {
          retry = true;
          Console.WriteLine($"{response.Text}\nPlease Try Again...\n");
        }
        else
        {
          retry = false;
          Console.WriteLine(response.Text);
        }
      } while (retry);
    }

    private static void DisplayCustomerOrderHistory()
    {
      // List all registered customers
      CustomerLogic customerLogic = new CustomerLogic();
      var listAllCustomersResponse = customerLogic.GetCustomerList();
      Console.WriteLine(listAllCustomersResponse.Text + "\n");

      // TODO: validate if user exists
      // Prompt user to select customer
      Console.WriteLine("From the list above please enter the name of the customer whose order history you wish to view");
      string usernameInput = Console.ReadLine();

      // Show results

      OrderLogic orderLogic = new OrderLogic();
      Console.WriteLine(orderLogic.FindOrdersByCustomerNamePacket(usernameInput).Text);
    }

    public static void PromptForCustomerPurchase()
    {
      // Display a list of customers and prompt user to select which customer will
      // be making a purchase
      CustomerLogic customerLogic = new CustomerLogic();
      var customerResponse = customerLogic.GetCustomerList();
      Console.WriteLine(customerResponse.Text);

      Console.WriteLine("From the list above choose the customer who will be making the purchase:");
      string usernameInput = Console.ReadLine();
      Console.WriteLine("\n");


      // Display list of locations that the selected customer will be making a purchase from
      // prompt user to choose which location to make a purchase from
      LocationLogic locationLogic = new LocationLogic();
      var locationsResponse = locationLogic.GetAllLocationsPacket();

      Console.WriteLine(locationsResponse.Text + "\n");

      Console.WriteLine("From the list above choose a location that you wish to make a purchase from:");
      string locationNameInput = Console.ReadLine();
      Console.WriteLine("\n");

      // Display the location's inventory, revealing a list of products and the 
      // quantity the current location posseses of those products
      // prompt user which product will be purchased and the amount to purchase
      ProductInventoryLogic pIL = new ProductInventoryLogic();
      var productInventoryResponse = pIL.RetrieveInventoryPacketByLocationName(locationNameInput);

      Console.WriteLine(productInventoryResponse.Text + "\n");

      Console.WriteLine("Please select a product from the list above to be purchsed:");
      string productNameInput = Console.ReadLine();
      Console.WriteLine("Select the amount:");
      string quantityInput = Console.ReadLine();


      // validate the quantity entered is a number
      int quantityParse;
      bool canConvertQauntity = Int32.TryParse(quantityInput, out quantityParse);
      if (!canConvertQauntity)
      {
        Console.WriteLine("Invalid input - quantity must be a number");
        return;
      }
      // perform the purchase and display the results to the console
      var purchaseResponse = customerLogic.MakePurchase(usernameInput, locationNameInput, productNameInput, quantityParse);
      Console.WriteLine(purchaseResponse.Text);
    }

    /// <summary>
    /// Displays a list of all the registered users. Then prompts the user to
    /// enter the name of which customer they want view the details on.
    /// </summary>
    private static void RequestForCustomerDetails()
    {
      // Print list of customers, store list to variable
      // Prompt for name to search for user details
      // validate if name is on the list
      // Print customer details
    }
  }
}