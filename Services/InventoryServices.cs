using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class InventoryServices
    {
        public bool OpenInventory()
        {
            var exit = false;
            while (!exit)
            {
                PrintInvMenu();
                switch (GameService.ParseIntput())
                {
                    case 1:
                        //-- Talk to Master
                        GameService.NewPage("You look at your items");
                        Console.ReadLine();
                        break;
                    case 2:
                        //-- Save and Quit
                        if (SaveAndQuit()) return true;
                        else break;
                    case 3:
                        //-- Exit Inventory
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        Console.ReadKey();
                        break;
                }
            }
            return false;
        }

        public void AccessChest()
        {

        }

        private bool SaveAndQuit()
        {
            var confirm = false;
            var confirmed = false;
            while (!confirmed)
            {
                GameService.NewPage("Are you sure you want to Save and Quit?" +
                    "\n1) Yes, save and exit" +
                    "\n2) No, return to game");
                confirmed = true;
                switch (GameService.ParseIntput())
                {
                    case 1:
                        confirm = true;
                        break;
                    case 2:
                        break;
                    default:
                        confirmed = false;
                        Console.WriteLine("Invalid input.");
                        Console.ReadKey();
                        break;
                }
            }
            return confirm;
        }

        private void PrintInvMenu()
        {
            GameService.NewPage("Inventory:" +
                "\nWhat would you like to do?" +
                "\n1) See Items" +
                "\n2) Save and Quit" +
                "\n3) Exit Inventory");
        }
    }
}
