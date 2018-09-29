﻿using Models;
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
        private Random rand = new Random();
        private ExploringServices exploringServices;

        public void Run()
        {
            exploringServices = new ExploringServices(rand);
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
            characterSuperModel = new CharacterSuperModel();
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
                return EnterVillage(n);
            }
            else
            {
                characterSuperModel.CurrentLocation = "city";
                return EnterCity();
            }
        }

        private bool EnterVillage(int num)
        {
            if (num != 0)
            {
                exploringServices.Commute();
                Console.ReadKey();
            }
            VillageServices villageServices = new VillageServices(characterSuperModel, exploringServices);
            return villageServices.RunMenu();
        }

        private bool EnterCity()
        {
            exploringServices.Commute();
            Console.ReadKey();
            CityServices cityServices = new CityServices(characterSuperModel, exploringServices);
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
