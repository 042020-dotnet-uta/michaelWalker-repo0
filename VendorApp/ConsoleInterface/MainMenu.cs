using System;

using VendorApp.BusinessLogic;

namespace VendorApp.ConsoleInterface
{
  // TODO add docs
  public class MainMenu
  {
    private bool isRunning = true;

    // Show main options for
    // TODO add docs
    public void Show()
    {
      // Give intro
      // List options to show the following
      // 1. Location Interface
      // 2. Customer Interface
      // 3. Products Interface

      string choice;

      while (isRunning)
      {
        Console.WriteLine(
@"
Main Menu - Please make a selection:
1. Location Menu
2. Customer Menu
3. Products Menu
0. Exit Application
"
        );

        choice = Console.ReadLine();

        switch (choice)
        {
          case "0":
            isRunning = false;
            break;
          case "1":
            LocationInterface.ShowLocationMenu();
            break;
          case "2":
            CustomerInterface.ShowCustomerMenu();
            break;
          case "3":
            CustomerInterface.ShowCustomerMenu();
            break;
          case "4":
            CustomerInterface.ShowCustomerMenu();
            break;
          default:
            Console.WriteLine("Invalid selection.  Please try again...\n");
            break;
        }
      }

      Console.WriteLine("Thank you for using VendorApp");
    }
  }
}