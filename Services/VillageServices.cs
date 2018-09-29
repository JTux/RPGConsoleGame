using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class VillageServices
    {
        public bool RunMenu()
        {
            bool leaveVillage = false;
            while (!leaveVillage)
            {
                PrintMenuOptions();
                var input = GameService.ParseIntput();
                switch (input)
                {
                    case 1:
                        //-- Talk to Master
                        GameService.NewPage("You go talk to your master");
                        Console.ReadKey();
                        break;
                    case 2:
                        //-- Go Home
                        GameService.NewPage("You go to your house");
                        GoHome();
                        break;
                    case 3:
                        //-- Inventory
                        bool leaveFromInv = OpenInventory();
                        if (leaveFromInv) return false;
                        break;
                    case 4:
                        //-- Leave Village
                        leaveVillage = LeaveVillage();
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        Console.ReadKey();
                        break;
                }
            }
            return leaveVillage;
        }

        private void GoHome()
        {
            var leaveHome = false;
            while (!leaveHome)
            {
                PrintHomeMenu();
                var input = GameService.ParseIntput();
                switch (input)
                {
                    case 1:
                        //-- Talk to Master
                        GameService.NewPage("Open chest inventory");
                        Console.ReadLine();
                        break;
                    case 2:
                        //-- Go Home
                        GameService.NewPage("You sleep in your bed");
                        Console.ReadLine();
                        break;
                    case 3:
                        //-- Leave Village
                        leaveHome = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private bool OpenInventory()
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

        private bool LeaveVillage()
        {
            var leave = false;
            var output = false;
            while (!leave)
            {
                PrintLeaveMenu();
                leave = true;
                switch (GameService.ParseIntput())
                {
                    case 1:
                        GameService.NewPage("You Explore the area");
                        Console.ReadKey();
                        output = false;
                        break;
                    case 2:
                        output = true;
                        break;
                    case 3:
                        output = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        leave = false;
                        Console.ReadKey();
                        break;
                }
            }
            return output;
        }

        private void PrintMenuOptions()
        {
            GameService.NewPage($"THE VILLAGE" +
                $"\n1) Visit your Master" +
                $"\n2) Go Home" +
                $"\n3) Open Inventory" +
                $"\n4) Leave Village");
        }
        private void PrintHomeMenu()
        {
            GameService.NewPage("Welcome home!" +
                "\nWhat would you like to do?" +
                "\n1) Access your Chest" +
                "\n2) Sleep in your Bed (+5hp)" +
                "\n3) Leave your Home");
        }
        private void PrintInvMenu()
        {
            GameService.NewPage("Inventory:" +
                "\nWhat would you like to do?" +
                "\n1) See Items" +
                "\n2) Save and Quit" +
                "\n3) Exit Inventory");
        }
        private void PrintLeaveMenu()
        {
            GameService.NewPage("Where will you go?" +
                "\n1) Explore the Surrounding Area" +
                "\n2) Travel to City" +
                "\n3) Return to Village");
        }
    }
}
