using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CityServices
    {
        public void RunMenu()
        {
            bool leaveCity = false;
            while (!leaveCity)
            {
                PrintMenuOptions();
                var input = GameService.ParseIntput();
                switch (input)
                {
                    case 1:
                        //-- Visit a Guild
                        GameService.NewPage("You go to a Guild");
                        Console.ReadKey();
                        break;
                    case 2:
                        //-- Fight in Arena
                        GameService.NewPage("You enter the arena");
                        Console.ReadKey();
                        break;
                    case 3:
                        //-- Sleep at Inn
                        GameService.NewPage("You sleep at the Inn");
                        Console.ReadKey();
                        break;
                    case 4:
                        //-- Shop at General Store
                        GameService.NewPage("You visit the general store");
                        Console.ReadKey();
                        break;
                    case 5:
                        //-- Leave City
                        leaveCity = true;
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
            GameService.NewPage($"THE CITY" +
                $"\n1) Visit a Guild" +
                $"\n2) Fight in the Arena" +
                $"\n3) Sleep at the Inn" +
                $"\n4) Shop at General Store" +
                $"\n5) Leave City");
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

        private void PrintHomeMenu()
        {
            GameService.NewPage("Welcome home!" +
                "\nWhat would you like to do?" +
                "\n1) Access your Chest" +
                "\n2) Sleep in your Bed (+5hp)" +
                "\n3) Leave your Home");
        }
    }
}
