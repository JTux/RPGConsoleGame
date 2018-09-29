using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class VillageServices
    {
        public void RunMenu()
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
                        Console.ReadLine();
                        break;
                    case 2:
                        //-- Go Home
                        GameService.NewPage("You go to your house");
                        GoHome();
                        break;
                    case 3:
                        //-- Inventory
                        GameService.NewPage("You open your bag");
                        break;
                    case 4:
                        //-- Leave Village
                        leaveVillage = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void PrintMenuOptions()
        {
            GameService.NewPage($"THE VILLAGE" +
                $"\n1) Visit your Master" +
                $"\n2) Go Home" +
                $"\n3) Open Inventory" +
                $"\n4) Leave Village");
            Console.SetCursorPosition(0, 6);
        }

        private void GoHome()
        {
            HomeMenu();
        }

        private void HomeMenu()
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
                        GameService.NewPage("You sleep in your bed.");
                        Console.ReadLine();
                        break;
                    case 3:
                        //-- Leave Village
                        leaveHome = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        Console.ReadKey();
                        break;
                }
            }

        }

        private void PrintHomeMenu()
        {
            GameService.NewPage("Welcome home!" +
                "\nWhat would you like to do?" +
                "\n1) Access your Chest." +
                "\n2) Sleep in your Bed. (+5hp)" +
                "\n3) Leave your Home.");
        }
    }
}
