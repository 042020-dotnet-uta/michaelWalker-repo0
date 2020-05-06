using System;

using VendorApp.DevTools;
using VendorApp.ConsoleInterface;

namespace VendorApp
{
  public class VendorApp
  {
    public void Start()
    {
      // DBSandbox DBS = new DBSandbox();

      // DBS.RunRounds();

      MainMenu menu = new MainMenu();

      menu.Show();
    }

    // Setup switch cases
  }
}