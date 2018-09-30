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

        public List<Equipment> GetEquipment()
        {
            List<Equipment> equipmentList = new List<Equipment>
            {
                new Equipment() { GearID = 1, GearName = "Bronze Sword", GearType = GearType.Melee, LvToUse = 10, BonusATK = 3, BonusHP = 0 },
                new Equipment() { GearID = 2, GearName = "Bronze Platebody", GearType = GearType.Melee, LvToUse = 10, BonusATK = 1, BonusHP = 5 },
                new Equipment() { GearID = 3, GearName = "Bronze Platelegs", GearType = GearType.Melee, LvToUse = 10, BonusATK = 1, BonusHP = 5 },

                new Equipment() { GearID = 4, GearName = "Iron Sword", GearType = GearType.Melee, LvToUse = 20, BonusATK = 6, BonusHP = 0 },
                new Equipment() { GearID = 5, GearName = "Iron Platebody", GearType = GearType.Melee, LvToUse = 20, BonusATK = 2, BonusHP = 10 },
                new Equipment() { GearID = 6, GearName = "Iron Platelegs", GearType = GearType.Melee, LvToUse = 20, BonusATK = 2, BonusHP = 10 },

                new Equipment() { GearID = 7, GearName = "Steel Sword", GearType = GearType.Melee, LvToUse = 30, BonusATK = 10, BonusHP = 0 },
                new Equipment() { GearID = 8, GearName = "Steel Platebody", GearType = GearType.Melee, LvToUse = 30, BonusATK = 3, BonusHP = 20 },
                new Equipment() { GearID = 9, GearName = "Steel Platelegs", GearType = GearType.Melee, LvToUse = 30, BonusATK = 3, BonusHP = 20 },


                new Equipment() { GearID = 11, GearName = "Short Bow", GearType = GearType.Ranged, LvToUse = 10, BonusATK = 3, BonusHP = 0 },
                new Equipment() { GearID = 12, GearName = "Leather Body", GearType = GearType.Ranged, LvToUse = 10, BonusATK = 1, BonusHP = 5 },
                new Equipment() { GearID = 13, GearName = "Leather Chaps", GearType = GearType.Ranged, LvToUse = 10, BonusATK = 1, BonusHP = 5 },

                new Equipment() { GearID = 14, GearName = "Long Bow", GearType = GearType.Ranged, LvToUse = 20, BonusATK = 6, BonusHP = 0 },
                new Equipment() { GearID = 15, GearName = "Hard-Leather Body", GearType = GearType.Ranged, LvToUse = 20, BonusATK = 2, BonusHP = 10 },
                new Equipment() { GearID = 16, GearName = "Hard-Leather Chaps", GearType = GearType.Ranged, LvToUse = 20, BonusATK = 2, BonusHP = 10 },

                new Equipment() { GearID = 17, GearName = "Compound Bow", GearType = GearType.Ranged, LvToUse = 30, BonusATK = 10, BonusHP = 0 },
                new Equipment() { GearID = 18, GearName = "Armored Body", GearType = GearType.Ranged, LvToUse = 30, BonusATK = 3, BonusHP = 20 },
                new Equipment() { GearID = 19, GearName = "Armored Chaps", GearType = GearType.Ranged, LvToUse = 30, BonusATK = 3, BonusHP = 20 },


                new Equipment() { GearID = 21, GearName = "Magic Staff", GearType = GearType.Mage, LvToUse = 10, BonusATK = 3, BonusHP = 0 },
                new Equipment() { GearID = 22, GearName = "Magic Robe", GearType = GearType.Mage, LvToUse = 10, BonusATK = 1, BonusHP = 5 },
                new Equipment() { GearID = 23, GearName = "Magic Skirt", GearType = GearType.Mage, LvToUse = 10, BonusATK = 1, BonusHP = 5 },

                new Equipment() { GearID = 24, GearName = "Elemental Staff", GearType = GearType.Mage, LvToUse = 20, BonusATK = 6, BonusHP = 0 },
                new Equipment() { GearID = 25, GearName = "Elemental Robe", GearType = GearType.Mage, LvToUse = 20, BonusATK = 2, BonusHP = 10 },
                new Equipment() { GearID = 26, GearName = "Elemental Skirt", GearType = GearType.Mage, LvToUse = 20, BonusATK = 2, BonusHP = 10 },

                new Equipment() { GearID = 27, GearName = "Mystic Staff", GearType = GearType.Mage, LvToUse = 30, BonusATK = 10, BonusHP = 0 },
                new Equipment() { GearID = 28, GearName = "Mystic Robe", GearType = GearType.Mage, LvToUse = 30, BonusATK = 3, BonusHP = 20 },
                new Equipment() { GearID = 29, GearName = "Mystic Skirt", GearType = GearType.Mage, LvToUse = 30, BonusATK = 3, BonusHP = 20 }
            };

            return equipmentList;
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