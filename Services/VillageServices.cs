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
        private InventoryServices inventory = new InventoryServices();
        private GameService gameService = new GameService();
        private CharacterSuperModel _characterSuperModel;

        public VillageServices(CharacterSuperModel characterSuperModel)
        {
            _characterSuperModel = characterSuperModel;
        }

        private int healthFromPlayerBed = 5;

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
                        bool leaveFromInv = inventory.OpenInventory();
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
                        _characterSuperModel.CharacterHealth += 5;
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
        private void PrintLeaveMenu()
        {
            GameService.NewPage("Where will you go?" +
                "\n1) Explore the Surrounding Area" +
                "\n2) Travel to City" +
                "\n3) Return to Village");
        }
    }
}
