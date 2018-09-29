using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GameService
    {
        private CharacterSuperModel characterSuperModel;
        private SaveServices saveServices;

        public void Run()
        {
            EnterVillage();
            EnterCity();
            RunMenu();
        }

        private void RunMenu()
        {
            bool closeApp = false;
            while (!closeApp)
            {
                PrintMenuOptions();
                var input = ParseIntput();
                switch (input)
                {
                    case 1:
                        //-- Load Game
                        NewPage("Choose Save Game:");
                        Console.ReadLine();
                        break;
                    case 2:
                        //-- New Game
                        //CreateGame();
                        break;
                    case 3:
                        //-- Tutorial
                        break;
                    case 4:
                        //-- Options
                        break;
                    case 5:
                        closeApp = true;
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
            NewPage($"MENU TITLE" +
                $"\n1) Continue" +
                $"\n2) New Game" +
                $"\n3) Tutorial" +
                $"\n4) Options" +
                $"\n5) Quit Game");
            Console.SetCursorPosition(0, 6);
        }

        private void EnterVillage()
        {
            VillageServices villageServices = new VillageServices();
            villageServices.RunMenu();
        }

        private void EnterCity()
        {
            CityServices cityServices = new CityServices();
            cityServices.RunMenu();
        }

        public static void NewPage(string prompt)
        {
            Console.Clear();
            Console.WriteLine(prompt);
        }

        public static int ParseIntput()
        {
            if (int.TryParse(Console.ReadLine(), out int value)) return value;
            else return 0;
        }
    }
}
