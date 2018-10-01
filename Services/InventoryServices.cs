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

        public InventoryServices() { }

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
                        PrintCurrentItems();
                        break;
                    case 2:
                        PrintCurrentAttacks();
                        break;
                    case 3:
                        ChooseCombatStyle();
                        break;
                    case 4:
                        if (SaveAndQuit()) return true;
                        else break;
                    case 5:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        Console.ReadKey();
                        break;
                }
            }
            _saveServices.SaveGame(_characterSuperModel);
            return false;
        }

        private void PrintCurrentItems()
        {
            var currentType = GearType.Melee;
            if (_characterSuperModel.CombatStyle == StyleType.Melee) currentType = GearType.Melee;
            else if (_characterSuperModel.CombatStyle == StyleType.Ranged) currentType = GearType.Ranged;
            else if (_characterSuperModel.CombatStyle == StyleType.Mage) currentType = GearType.Mage;

            GameService.NewPage("\nYou look at your items" +
                            $"\n{"ID",-2}  {"Name",-18}  {"Type",-6}  {"Lvl",-4}  {"ATK+",-4}  {"HP+",-4}", "inv");
            foreach (Equipment item in _characterSuperModel.CharacterEquipment.Where(i => i.GearType == currentType))
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }

        private void PrintCurrentAttacks()
        {
            var currentType = AtkType.Melee;
            if (_characterSuperModel.CombatStyle == StyleType.Melee) currentType = AtkType.Melee;
            else if (_characterSuperModel.CombatStyle == StyleType.Ranged) currentType = AtkType.Ranged;
            else if (_characterSuperModel.CombatStyle == StyleType.Mage) currentType = AtkType.Mage;

            GameService.NewPage("\nYou look at your items" +
                            $"\n{"ID",-2}  {"Name",-20}  {"Type",-6}  {"Lvl",-3}  {"Stun",-5}  {"D.O.T.",-6}  {"Stealth",-5}", "inv");
            foreach (Attacks attack in _characterSuperModel.CharacterAttacks.Where(i => i.TypeOfAtk == currentType))
            {
                Console.WriteLine(attack);
            }
            Console.ReadLine();
        }

        private void ChooseCombatStyle()
        {
            var exit = false;
            while (!exit)
            {
                exit = true;
                PrintStyleMenu();
                switch (GameService.ParseIntput())
                {
                    case 1:
                        _characterSuperModel.CombatStyle = StyleType.Melee;
                        break;
                    case 2:
                        _characterSuperModel.CombatStyle = StyleType.Ranged;
                        break;
                    case 3:
                        _characterSuperModel.CombatStyle = StyleType.Mage;
                        break;
                    default:
                        exit = false;
                        Console.WriteLine("Invalid input");
                        Console.ReadKey();
                        break;
                }
            }
            Console.WriteLine("Style set!");
            Console.ReadKey();
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


        private void PrintStyleMenu()
        {
            GameService.NewPage($"\nYour current Combat Style is set to {_characterSuperModel.CombatStyle}." +
                "\n\nWhich combat style would you like to focus on?" +
                "\n1) Melee" +
                "\n2) Ranged" +
                "\n3) Mage", "inv");
        }
        private void PrintInvMenu()
        {
            GameService.NewPage($"\n{GameService.GetCharacterStats(_characterSuperModel)}" +
                "\n\nWhat would you like to do?" +
                $"\n1) See owned {_characterSuperModel.CombatStyle} Items" +
                $"\n2) See known {_characterSuperModel.CombatStyle} Attacks" +
                "\n3) Choose Combat Style" +
                "\n4) Save and Quit" +
                "\n5) Exit Inventory", "inv");
        }
        private void PrintChestMenu()
        {
            GameService.NewPage("\nWhat would you like to do?" +
                "\n1) Take Items" +
                "\n2) Store Items" +
                "\n3) Exit Chest", "chest");
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
        public List<Attacks> GetAttacks()
        {
            List<Attacks> listOfATKS = new List<Attacks>();

            listOfATKS.Add(new Attacks() { ATKID = 1, ATKName = "Pierce", TypeOfAtk = AtkType.Melee, DMG = 3, LVToUSE = 1 });
            listOfATKS.Add(new Attacks() { ATKID = 2, ATKName = "Blinding Rage", TypeOfAtk = AtkType.Melee, DMG = 4, LVToUSE = 5 });
            listOfATKS.Add(new Attacks() { ATKID = 3, ATKName = "Ambush", TypeOfAtk = AtkType.Melee, DMG = 5, LVToUSE = 10 });
            listOfATKS.Add(new Attacks() { ATKID = 4, ATKName = "Double Daggers", TypeOfAtk = AtkType.Melee, DMG = 6, LVToUSE = 15 });
            listOfATKS.Add(new Attacks() { ATKID = 5, ATKName = "Leap", TypeOfAtk = AtkType.Melee, DMG = 7, LVToUSE = 20, Stun = true });
            listOfATKS.Add(new Attacks() { ATKID = 6, ATKName = "Takedown", TypeOfAtk = AtkType.Melee, DMG = 8, LVToUSE = 25 });
            listOfATKS.Add(new Attacks() { ATKID = 7, ATKName = "Poison Shiv", TypeOfAtk = AtkType.Melee, DMG = 9, LVToUSE = 30, DamageOverTime = true });
            listOfATKS.Add(new Attacks() { ATKID = 8, ATKName = "Shadow Assault", TypeOfAtk = AtkType.Melee, DMG = 10, LVToUSE = 35, Stealth = true });


            listOfATKS.Add(new Attacks() { ATKID = 11, ATKName = "Volley", TypeOfAtk = AtkType.Ranged, DMG = 3, LVToUSE = 1 });
            listOfATKS.Add(new Attacks() { ATKID = 12, ATKName = "Rapid Fire", TypeOfAtk = AtkType.Ranged, DMG = 4, LVToUSE = 5 });
            listOfATKS.Add(new Attacks() { ATKID = 13, ATKName = "Hail of Arrows", TypeOfAtk = AtkType.Ranged, DMG = 5, LVToUSE = 10 });
            listOfATKS.Add(new Attacks() { ATKID = 14, ATKName = "Trueshot", TypeOfAtk = AtkType.Ranged, DMG = 6, LVToUSE = 15 });
            listOfATKS.Add(new Attacks() { ATKID = 15, ATKName = "Quickdraw", TypeOfAtk = AtkType.Ranged, DMG = 7, LVToUSE = 20 });
            listOfATKS.Add(new Attacks() { ATKID = 16, ATKName = "Piercing Arrow", TypeOfAtk = AtkType.Ranged, DMG = 8, LVToUSE = 25, Stun = true });
            listOfATKS.Add(new Attacks() { ATKID = 17, ATKName = "Toxic Shot", TypeOfAtk = AtkType.Ranged, DMG = 9, LVToUSE = 30, DamageOverTime = true });
            listOfATKS.Add(new Attacks() { ATKID = 18, ATKName = "Blinding Dart", TypeOfAtk = AtkType.Ranged, DMG = 10, LVToUSE = 35, Stealth = true });


            listOfATKS.Add(new Attacks() { ATKID = 21, ATKName = "Mystic Shot", TypeOfAtk = AtkType.Mage, DMG = 3, LVToUSE = 1 });
            listOfATKS.Add(new Attacks() { ATKID = 22, ATKName = "Rain of Smoke", TypeOfAtk = AtkType.Mage, DMG = 4, LVToUSE = 5 });
            listOfATKS.Add(new Attacks() { ATKID = 23, ATKName = "Shroud of Darkness", TypeOfAtk = AtkType.Mage, DMG = 5, LVToUSE = 10, Stealth = true });
            listOfATKS.Add(new Attacks() { ATKID = 24, ATKName = "Piercing Light", TypeOfAtk = AtkType.Mage, DMG = 6, LVToUSE = 15 });
            listOfATKS.Add(new Attacks() { ATKID = 25, ATKName = "Lightning artillery", TypeOfAtk = AtkType.Mage, DMG = 7, LVToUSE = 20 });
            listOfATKS.Add(new Attacks() { ATKID = 26, ATKName = "Ardent Blaze", TypeOfAtk = AtkType.Mage, DMG = 8, LVToUSE = 25 });
            listOfATKS.Add(new Attacks() { ATKID = 27, ATKName = "Contaminate", TypeOfAtk = AtkType.Mage, DMG = 9, LVToUSE = 30, DamageOverTime = true });
            listOfATKS.Add(new Attacks() { ATKID = 28, ATKName = "Ethereal Chains", TypeOfAtk = AtkType.Mage, DMG = 10, LVToUSE = 35, Stun = true });

            return listOfATKS;
        }
    }
}