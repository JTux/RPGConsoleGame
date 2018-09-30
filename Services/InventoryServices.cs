using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class InventoryServices
    {
        private SaveServices _saveServices = new SaveServices();
        private CharacterSuperModel _characterSuperModel;

        public InventoryServices(CharacterSuperModel superModel)
        {
            _characterSuperModel = superModel;
        }

        public bool OpenInventory()
        {
            var exit = false;
            while (!exit)
            {
                PrintInvMenu();
                switch (GameService.ParseIntput())
                {
                    case 1:
                        GameService.NewPage("\nYou look at your items", "inv");
                        Console.ReadLine();
                        break;
                    case 2:
                        //-- Save and Quit
                        if (SaveAndQuit()) return true;
                        else break;
                    case 3:
                        //-- Exit Inventory
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        Console.ReadKey();
                        break;
                }
            }
            return false;
        }

        public void AccessChest()
        {
            var exit = false;
            while (!exit)
            {
                PrintChestMenu();
                switch (GameService.ParseIntput())
                {
                    case 1:
                        GameService.NewPage("\nWhich item would you like to take?", "chest");
                        Console.ReadKey();
                        break;
                    case 2:
                        GameService.NewPage("\nWhich item would you like to store?", "chest");
                        Console.ReadKey();
                        break;
                    case 3:
                        //-- Exit Inventory
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private bool SaveAndQuit()
        {
            var confirm = false;
            var confirmed = false;
            while (!confirmed)
            {
                GameService.NewPage("\nAre you sure you want to Save and Quit?" +
                    "\n1) Yes I want to save and quit" +
                    "\n2) No I want to return to the game", "saveQuit");
                confirmed = true;
                switch (GameService.ParseIntput())
                {
                    case 1:
                        _saveServices.SaveGame(_characterSuperModel);
                        confirm = true;
                        break;
                    case 2:
                        break;
                    default:
                        confirmed = false;
                        Console.WriteLine("Invalid input.");
                        Console.ReadKey();
                        break;
                }
            }
            return confirm;
        }

        private void PrintInvMenu()
        {
            GameService.NewPage("\nWhat would you like to do?" +
                "\n1) See Items" +
                "\n2) Save and Quit" +
                "\n3) Exit Inventory", "inv");
        }
        private void PrintChestMenu()
        {
            GameService.NewPage("\nWhat would you like to do?" +
                "\n1) Take Items" +
                "\n2) Store Items" +
                "\n3) Exit Chest", "chest");
        }
    }
}