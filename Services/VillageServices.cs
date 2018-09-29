using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class VillageServices
    {
        private InventoryServices _inventoryServices = new InventoryServices();
        private ExploringServices _exploringServices;
        private CharacterSuperModel _characterSuperModel;

        public VillageServices(CharacterSuperModel characterSuperModel, ExploringServices exploringServices)
        {
            _characterSuperModel = characterSuperModel;
            _exploringServices = exploringServices;
        }

        private int healthFromPlayerBed = 5;

        public bool RunMenu()
        {
            var leaveVillage = false;
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
                        GoHome();
                        break;
                    case 3:
                        //-- Inventory
                        bool leaveFromInv = _inventoryServices.OpenInventory();
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
                        _inventoryServices.AccessChest();
                        break;
                    case 2:
                        //-- Go Home
                        _characterSuperModel.CharacterHealth += healthFromPlayerBed;
                        GameService.NewPage($"You sleep in your bed and recover {healthFromPlayerBed} HP." +
                            $"\nYou now have {_characterSuperModel.CharacterHealth} HP.");
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
                        _exploringServices.Explore();
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
            GameService.NewPage($"Welcome home!" +
                $"\nWhat would you like to do?" +
                $"\n1) Access your Chest" +
                $"\n2) Sleep in your Bed (+{healthFromPlayerBed} HP)" +
                $"\n3) Leave your Home");
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
