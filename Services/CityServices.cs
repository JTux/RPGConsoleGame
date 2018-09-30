using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CityServices
    {
        private InventoryServices _inventoryServices;
        private ExploringServices _exploringServices;
        private CharacterSuperModel _characterSuperModel;

        public CityServices(CharacterSuperModel characterSuperModel, ExploringServices exploringServices)
        {
            _characterSuperModel = characterSuperModel;
            _exploringServices = exploringServices;
            _inventoryServices = new InventoryServices(_characterSuperModel);
        }

        private int healthFromInnBed = 8;

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
                        GuildMenu();
                        break;
                    case 2:
                        //-- Fight in Arena
                        GameService.NewPage("You enter the arena", "arena");
                        Console.ReadKey();
                        break;
                    case 3:
                        //-- Sleep at Inn
                        _characterSuperModel.CharacterHealth += healthFromInnBed;
                        GameService.NewPage($"You sleep in a comfy bed at the inn and recover {healthFromInnBed} HP." +
                            $"\nYou now have {_characterSuperModel.CharacterHealth}/{_characterSuperModel.CharacterMaxHealth} HP.");
                        Console.ReadKey();
                        break;
                    case 4:
                        bool leaveFromInv = _inventoryServices.OpenInventory();
                        if (leaveFromInv) return false;
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

        private void GuildMenu()
        {
            bool leaveGuild = false;
            while (!leaveGuild)
            {
                PrintGuildMenu();
                switch (GameService.ParseIntput())
                {
                    case 1:
                        GameService.NewPage("Shop!", "shop");
                        Console.ReadKey();
                        break;
                    case 2:
                        GameService.NewPage("Master Archer", "archerGuild");
                        Console.ReadKey();
                        break;
                    case 3:
                        GameService.NewPage("Master Swordsman", "meleeGuild");
                        Console.ReadKey();
                        break;
                    case 4:
                        GameService.NewPage("Master Mage", "mageGuild");
                        Console.ReadKey();
                        break;
                    case 5:
                        leaveGuild = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        Console.ReadKey();
                        break;
                }
            }
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
            GameService.NewPage($"\n1) Visit the Combat Guild" +
                $"\n2) Fight in the Arena" +
                $"\n3) Sleep at the Inn (+{healthFromInnBed} HP)" +
                $"\n4) Open Inventory" +
                $"\n5) Leave City", "city");
        }
        private void PrintGuildMenu()
        {
            GameService.NewPage("\n1) Shop for Gear" +
                "\n2) Speak to Master Archer" +
                "\n3) Speak to Master Swordsman" +
                "\n4) Speak to Master Mage" +
                "\n5) Return to City", "guild");
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
