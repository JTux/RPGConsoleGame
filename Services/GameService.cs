﻿using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GameService
    {
        private CharacterSuperModel characterSuperModel;
        private SaveServices saveServices = new SaveServices();
        private Random rand = new Random();
        private ExploringServices exploringServices;

        public void Run()
        {
            saveServices.LoadSettings();
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
                        LoadGame();
                        break;
                    case 2:
                        //-- New Game
                        CreateNewGame();
                        break;
                    case 3:
                        //-- Tutorial
                        break;
                    case 4:
                        //-- Options
                        OptionsMenu();
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

        private void LoadGame()
        {
            var foundGame = false;
            while (!foundGame)
            {
                NewPage("Choose Save Game:");
                saveServices.PrintSaves();
                var saveID = ParseIntput();
                if (saveID == (SaveServices.SaveGames + 1)) break;
                else if (saveID > 0 && saveID <= SaveServices.SaveGames && File.Exists($"./Files/Saves/Game{saveID}.txt"))
                {
                    foundGame = true;
                    Play(saveServices.LoadSave(saveID));
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    Console.ReadKey();
                }
            }
        }

        private void CreateNewGame()
        {
            NewPage("New Game:" +
                "\nWhat's your name?");
            string newName;
            while (true)
            {
                newName = Console.ReadLine();
                if (newName != "" && !newName.Contains(",")) break;
                else Console.Write("Enter valid name: ");
            }

            SaveServices.SaveGames++;
            characterSuperModel = new CharacterSuperModel
            {
                CharacterID = SaveServices.SaveGames,
                CharacterName = newName,
                CharacterBaseHealth = 10,
                CharacterHealth = 10,
                CharacterMaxHealth = 10
            };
            saveServices.SaveGame(characterSuperModel);
            Play(characterSuperModel);
        }

        private void Play(CharacterSuperModel character)
        {
            characterSuperModel = character;
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
                if (n != 0)
                {
                    exploringServices.Commute();
                    Console.ReadKey();
                }
                return EnterVillage();
            }
            else
            {
                exploringServices.Commute();
                Console.ReadKey();
                characterSuperModel.CurrentLocation = "city";
                return EnterCity();
            }
        }

        private bool EnterVillage()
        {
            saveServices.SaveGame(characterSuperModel);
            VillageServices villageServices = new VillageServices(characterSuperModel, exploringServices);
            return villageServices.RunMenu();
        }

        private bool EnterCity()
        {
            saveServices.SaveGame(characterSuperModel);
            CityServices cityServices = new CityServices(characterSuperModel, exploringServices);
            return cityServices.RunMenu();
        }

        private void OptionsMenu()
        {
            bool closeOptions = false;
            while (!closeOptions)
            {
                PrintOptionsOptions();
                closeOptions = true;
                switch (ParseIntput())
                {
                    case 1:
                        //-- Load Game
                        saveServices.Reset();
                        Console.WriteLine("Files Reset");
                        Console.ReadKey();
                        break;
                    case 2:
                        break;
                    default:
                        closeOptions = false;
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
        private void PrintOptionsOptions()
        {
            NewPage($"Options:" +
                $"\n1) Reset Save Files" +
                $"\n2) Return to Menu");
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
