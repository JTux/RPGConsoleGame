﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class VillageServices
    {
        private InventoryServices _inventoryServices;
        private ExploringServices _exploringServices;
        private CharacterSuperModel _characterSuperModel;
        private SaveServices _saveServices = new SaveServices();

        public VillageServices(CharacterSuperModel characterSuperModel, ExploringServices exploringServices)
        {
            _characterSuperModel = characterSuperModel;
            _exploringServices = exploringServices;
            _inventoryServices = new InventoryServices(_characterSuperModel);
        }

        private int healthFromPlayerBed = 5;

        public bool RunMenu()
        {
            var leaveVillage = false;
            while (!leaveVillage)
            {
                PrintMenuOptions();
                _saveServices.SaveGame(_characterSuperModel);
                var input = GameService.ParseIntput();
                switch (input)
                {
                    case 1:
                        GameService.NewPage("You go talk to your master");
                        Console.ReadKey();
                        break;
                    case 2:
                        GoHome();
                        break;
                    case 3:
                        bool leaveFromInv = _inventoryServices.OpenInventory();
                        if (leaveFromInv) return false;
                        break;
                    case 4:
                        leaveVillage = Leave();
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
                        _inventoryServices.AccessChest();
                        break;
                    case 2:
                        if ((_characterSuperModel.CharacterBaseHealth += healthFromPlayerBed) > _characterSuperModel.CharacterMaxHealth)
                            _characterSuperModel.CharacterHealth = _characterSuperModel.CharacterMaxHealth;
                        else
                            _characterSuperModel.CharacterHealth += healthFromPlayerBed;
                        GameService.NewPage($"You sleep in your bed and recover {healthFromPlayerBed} HP." +
                            $"\nYou now have {_characterSuperModel.CharacterHealth}/{_characterSuperModel.CharacterMaxHealth} HP.");
                        Console.ReadLine();
                        break;
                    case 3:
                        leaveHome = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private bool Leave()
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
                        var isDead = _exploringServices.Explore();
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
            GameService.NewPage($"\nAh. Home sweet home. It's a perfectly boring small village but it's all you've known for most of your life.\nA small collection of huts filled with familiar faces. A few small children are running around playing a game.\nIt sure is good to be home. While you may be forced to travel now this will always be home.\nOne of the children sees you and yells \"Welcome home {_characterSuperModel.CharacterName}!\"" +
                $"\n\n{GameService.GetCharacterStats(_characterSuperModel)}" +
                $"\n\nWhat do you do?" +
                $"\n\n1) Visit your Master" +
                $"\n2) Go Home" +
                $"\n3) Open Inventory" +
                $"\n4) Leave Village", "village");
        }
        private void PrintHomeMenu()
        {
            GameService.NewPage($"\nWelcome home!" +
                $"\nWhat would you like to do?" +
                $"\n1) Access your Chest" +
                $"\n2) Sleep in your Bed (+{healthFromPlayerBed} HP)" +
                $"\n3) Leave your Home", "home");
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
