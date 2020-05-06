using System;

using VendorApp.BusinessLogic;

namespace VendorApp.ConsoleInterface
{
  // TODO add docs
  public class LocationInterface
  {
    // TODO: add docs
    private static bool isRunning;

    //TODO add docs
    public static void ShowLocationMenu()
    {
      isRunning = true;
      // TODO Implement Interface
      // 1. ✅ List Locations
      // 2  List Location's Order history
      // 2. Make purchase
      // 3. Restock

      do
      {
        Console.WriteLine(
@"Select a Location choice:
1. List locations
2. Display a location's inventory
3. Display location's order history
0. Back to main menu
");
        string choice = Console.ReadLine();

        switch (choice)
        {
          case "0":
            isRunning = false;
            break;
          case "1":
            Console.WriteLine("\n");
            DisplayLocations();
            Console.WriteLine("\n");
            break;
          case "2":
            Console.WriteLine("\n");
            DisplayLocationsInventory();
            Console.WriteLine("\n");
            break;
          case "3":
            Console.WriteLine("\n");
            DisplayLocationsOrderHistory();
            Console.WriteLine("\n");
            break;
          default:
            Console.WriteLine("Try again");
            break;
        }
      } while (isRunning);

      // Give option to view Location's inventory
      // Give options to list inventory by Product Name/Catagory

    }

    /// <summary>
    /// Display a list of all registered locations to the console
    /// </summary>
    private static void DisplayLocations()
    {
      LocationLogic locationLogic = new LocationLogic();
      Packet LocationList = locationLogic.GetAllLocationsPacket();

      Console.WriteLine(LocationList.Text);
    }
    private static void DisplayLocationsInventory()
    {
      // List all locations
      DisplayLocations();

      // Prompt user to enter the name of the location they want to
      // view the order history on
      Console.WriteLine("Please enter the name of the location from the list above to view their inventory");

      string locationNameInput = Console.ReadLine();
      LocationLogic locationLogic = new LocationLogic();
      Packet response = locationLogic.GetLocationInventoryByNamePacket(locationNameInput);

      Console.WriteLine(response.Text);
    }

    /// <summary>
    /// Prompts the user to enter the name of a location from a provided list.
    /// Display the order history of the chosen location.
    /// </summary>
    private static void DisplayLocationsOrderHistory()
    {
      // List all locations
      DisplayLocations();

      // Prompt user to enter the name of the location they want to
      // view the order history on
      Console.WriteLine("Please enter the name of the location from the list above to view their order history");

      string locationNameInput = Console.ReadLine();
      LocationLogic locationLogic = new LocationLogic();
      var fetchOrderHistoryResponse = locationLogic.GetLocationOrderHistoryPacket(locationNameInput);

      // Display the order history for that location
      Console.WriteLine(fetchOrderHistoryResponse.Text);
    }
  }
}