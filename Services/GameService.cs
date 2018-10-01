using Data;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class GameService
    {
        private CharacterSuperModel characterSuperModel;
        private SaveServices saveServices = new SaveServices();
        private Random rand = new Random();
        private Dialogue dialogue = new Dialogue();
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
                if (SaveServices.SaveGames == 0)
                {
                    switch (ParseIntput())
                    {
                        case 1:
                            NewGame();
                            break;
                        case 2:
                            //-- Tutorial
                            break;
                        case 3:
                            OptionsMenu();
                            break;
                        case 4:
                            closeApp = true;
                            break;
                        default:
                            Console.WriteLine("Invalid input.");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    switch (ParseIntput())
                    {
                        case 1:
                            LoadGame();
                            break;
                        case 2:
                            NewGame();
                            break;
                        case 3:
                            //-- Tutorial
                            break;
                        case 4:
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
        }

        private void LoadGame()
        {
            var foundGame = false;
            while (!foundGame)
            {
                NewPage("\nChoose Save Game:", "loadGame");
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

        private void NewGame()
        {
            var exit = false;
            while (!exit)
            {
                PrintConfirmNewGame();
                exit = true;
                switch (ParseIntput())
                {
                    case 1:
                        CreateNewGame();
                        break;
                    case 2:
                        break;
                    default:
                        exit = false;
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }

        private void CreateNewGame()
        {
            NewPage("\nWhat's your name?", "newGame");
            string newName;
            StyleType newCombatStyle = StyleType.Melee;
            while (true)
            {
                newName = Console.ReadLine();
                if (newName != "" && !newName.Contains(",")) break;
                else Console.Write("Enter valid name: ");
            }

            PrintFirstStylePicker();
            var pickingStyle = true;
            while (pickingStyle)
            {
                pickingStyle = false;
                switch (ParseIntput())
                {
                    case 1:
                        newCombatStyle = StyleType.Melee;
                        break;
                    case 2:
                        newCombatStyle = StyleType.Ranged;
                        break;
                    case 3:
                        newCombatStyle = StyleType.Mage;
                        break;
                    default:
                        pickingStyle = true;
                        Console.WriteLine("Invalid input");
                        Console.ReadKey();
                        break;
                }
            }

            SaveServices.SaveGames++;
            characterSuperModel = new CharacterSuperModel
            {
                CharacterID = SaveServices.SaveGames,
                CharacterName = newName,
                CharacterLevel = 1,
                CharacterBaseHealth = 10,
                CharacterHealth = 10,
                CharacterMaxHealth = 10,
                CombatStyle = newCombatStyle,
            };
            characterSuperModel.CharacterAttacks.Add(new Attacks() { ATKID = 1, ATKName = "Pierce", TypeOfAtk = AtkType.Melee, DMG = 3, LVToUSE = 1 });
            characterSuperModel.CharacterAttacks.Add(new Attacks() { ATKID = 11, ATKName = "Volley", TypeOfAtk = AtkType.Ranged, DMG = 3, LVToUSE = 1 });
            characterSuperModel.CharacterAttacks.Add(new Attacks() { ATKID = 21, ATKName = "Mystic Shot", TypeOfAtk = AtkType.Mage, DMG = 3, LVToUSE = 1 });

            FirstTimeStart(newName);
            saveServices.SaveGame(characterSuperModel);
            Play(characterSuperModel);
        }

        private void FirstTimeStart(string playerName)
        {
            NewPage(dialogue.StartText(playerName));
            Console.WriteLine("\nPress any button to begin your journey...");
            Console.ReadKey();
        }

        private void Play(CharacterSuperModel character)
        {
            characterSuperModel = character;
            var counter = 0;
            if (character.CurrentLocation == "city") counter = 1;
            var keepPlaying = true;
            exploringServices = new ExploringServices(rand, characterSuperModel);
            while (keepPlaying)
            {
                keepPlaying = SetLocation(counter);
                counter += 3;
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
                characterSuperModel.CurrentLocation = "city";
                if (n != 1)
                {
                    exploringServices.Commute();
                    Console.ReadKey();
                }
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
            if (SaveServices.SaveGames != 0)
            {
                NewPage($"\n1) Continue" +
                    $"\n2) New Game" +
                    $"\n3) Tutorial" +
                    $"\n4) Options" +
                    $"\n5) Quit Game", "title");
            }
            else
            {
                NewPage($"\n1) New Game" +
                    $"\n2) Tutorial" +
                    $"\n3) Options" +
                    $"\n4) Quit Game", "title");
            }
        }
        private void PrintOptionsOptions()
        {
            NewPage($"\n1) Reset Save Files" +
                $"\n2) Return to Menu", "options");
        }
        private void PrintConfirmNewGame()
        {
            NewPage("\nDo you want to start a new game?" +
                "\n1) Yes begin new game" +
                "\n2) No return to menu", "newGame");
        }
        private void PrintFirstStylePicker()
        {
            Console.WriteLine($"Welcome. In this game there are three different combat styles which include different attacks and weapons." +
                "\nWhile one style can be utilized at any given time, you can change this at any time in your inventory." +
                "\nWhich combat style would you like to start with?" +
                "\n1) Melee" +
                "\n2) Ranged" +
                "\n3) Mage");
        }

        public static string GetCharacterStats(CharacterSuperModel currentCharacter)
        {
            return($"You are currently Level {currentCharacter.CharacterLevel}.\n" +
                $"You have {currentCharacter.CharacterHealth}/{currentCharacter.CharacterMaxHealth} HP and have {currentCharacter.Gold} Gold.\n" +
                $"You are currently focusing on {currentCharacter.CombatStyle} styled combat.");
        }
        public static void NewPage(string prompt)
        {
            Console.Clear();
            Console.WriteLine(prompt);
        }
        public static void NewPage(string prompt, string headerKey)
        {
            Console.Clear();
            PrintHeader(headerKey);
            Console.WriteLine(prompt);
        }
        public static int ParseIntput()
        {
            if (int.TryParse(Console.ReadLine(), out int value)) return value;
            else return 0;
        }
        private static void PrintHeader(string key)
        {
            HeaderText headerText = new HeaderText();
            var text = headerText.GetHeader(key);
            string[] lines = text.Split(';');
            foreach (string line in lines) Console.WriteLine(line);
        }
    }
}