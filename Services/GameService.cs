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
        private CharacterSuperModel characterSuperModel = new CharacterSuperModel();
        private SaveServices saveServices;

        public void Run()
        {
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
                        Play();
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

        private void Play()
        {
            var counter = 0;
            var keepPlaying = true;
            while (keepPlaying)
            {
                keepPlaying = SetLocation(counter);
                counter++;
            }
        }

        private bool SetLocation(int n)
        {
            if (n % 2 == 0)
            {
                characterSuperModel.CurrentLocation = "village";
                return EnterVillage();
            }
            else
            {
                characterSuperModel.CurrentLocation = "city";
                return EnterCity();
            }
        }

        private bool EnterVillage()
        {
            VillageServices villageServices = new VillageServices(characterSuperModel);
            return villageServices.RunMenu();
        }

        private bool EnterCity()
        {
            CityServices cityServices = new CityServices();
            return cityServices.RunMenu();
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
