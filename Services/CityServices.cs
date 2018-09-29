using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CityServices
    {
        public bool RunMenu()
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
                        leaveCity = LeaveCity();
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        Console.ReadKey();
                        break;
                }
            }
            return leaveCity;
        }

        private bool LeaveCity()
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
            GameService.NewPage($"THE CITY" +
                $"\n1) Visit a Guild" +
                $"\n2) Fight in the Arena" +
                $"\n3) Sleep at the Inn" +
                $"\n4) Shop at General Store" +
                $"\n5) Leave City");
        }
        private void PrintLeaveMenu()
        {
            GameService.NewPage("Where will you go?" +
                "\n1) Explore the Surrounding Area" +
                "\n2) Travel to Village" +
                "\n3) Return to City");
        }
    }
}
