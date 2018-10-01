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
        private List<Equipment> _shopEquipmentList;
        private SaveServices _saveServices = new SaveServices();
        private CombatService _combatService;

        public CityServices(CharacterSuperModel characterSuperModel, ExploringServices exploringServices)
        {
            _characterSuperModel = characterSuperModel;
            _exploringServices = exploringServices;
            _inventoryServices = new InventoryServices(_characterSuperModel);
            _shopEquipmentList = _inventoryServices.GetEquipment();
            _combatService = new CombatService(_characterSuperModel);
        }

        private int healthFromInnBed = 8;

        public bool RunMenu()
        {
            bool leaveCity = false;
            while (!leaveCity)
            {
                PrintMenuOptions();
                _saveServices.SaveGame(_characterSuperModel);
                var input = GameService.ParseIntput();
                switch (input)
                {
                    case 1:
                        GuildMenu();
                        break;
                    case 2:
                        EnterArena();
                        if (_characterSuperModel.IsDead)
                        {
                            GameService.NewPage("You slowly open your eyes. \"Wha.. what happened?\"\n" +
                            "Near your bed you find a note a note. It reads:\n\n" +
                            "\"You got smashed bud. Maybe you should train a bit more before you enter the arena again.\"");
                            _characterSuperModel.CharacterHealth = _characterSuperModel.CharacterMaxHealth;
                            Console.ReadKey();
                        }
                        break;
                    case 3:
                        if ((_characterSuperModel.CharacterBaseHealth += healthFromInnBed) > _characterSuperModel.CharacterMaxHealth)
                            _characterSuperModel.CharacterHealth = _characterSuperModel.CharacterMaxHealth;
                        else
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
                        leaveCity = Leave();
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        Console.ReadKey();
                        break;
                }
            }
            return leaveCity;
        }

        private void EnterArena()
        {
            GameService.NewPage("You enter the arena", "arena");
            _combatService.ArenaFight(_characterSuperModel);
            if (_characterSuperModel.IsDead)
            {
                Console.WriteLine("YOU DIED");
                Console.ReadKey();
            }
        }

        private void GuildMenu()
        {
            bool leaveGuild = false;
            while (!leaveGuild)
            {
                PrintGuildMenu();
                _saveServices.SaveGame(_characterSuperModel);
                switch (GameService.ParseIntput())
                {
                    case 1:
                        GuildStore();
                        break;
                    case 2:
                        VisitGuildMaster("Melee");
                        break;
                    case 3:
                        VisitGuildMaster("Ranged");
                        break;
                    case 4:
                        VisitGuildMaster("Mage");
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

        private void VisitGuildMaster(string guildName)
        {
            var guildKey = "";
            switch (guildName)
            {
                case "Melee":
                    guildKey = "meleeGuild";
                    break;
                case "Ranged":
                    guildKey = "archerGuild";
                    break;
                case "Mage":
                    guildKey = "mageGuild";
                    break;
            }

            var exit = false;
            while (!exit)
            {
                exit = true;

                GameService.NewPage($"\nYou approach the {guildName} Guild Master and..." +
                    $"\n1) Learn new Attacks" +
                    $"\n2) Leave", guildKey);
                switch (GameService.ParseIntput())
                {
                    case 1:
                        LearnAttacks(_characterSuperModel, guildName);
                        break;
                    case 2:
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void LearnAttacks(CharacterSuperModel character, string guildName)
        {
            var invService = new InventoryServices();
            var attackList = invService.GetAttacks();
            var count = 0;
            var correctType = AtkType.Melee;
            switch (guildName)
            {
                case "Melee":
                    correctType = AtkType.Melee;
                    break;
                case "Ranged":
                    correctType = AtkType.Ranged;
                    break;
                case "Mage":
                    correctType = AtkType.Mage;
                    break;
            }
            foreach (Attacks attack in attackList)
            {
                if (attack.LVToUSE <= character.CharacterLevel && attack.TypeOfAtk == correctType && _characterSuperModel.CharacterAttacks.Where(a => a.ATKID == attack.ATKID) == null)
                {
                    count++;
                    character.CharacterAttacks.Add(attack);
                }
            }
            if (count != 0)
            {
                var s = "";
                if (count != 1) s = "s";
                Console.WriteLine($"You learned {count} new attack{s}");
            }
            else Console.WriteLine($"You are not ready to learn any new attacks yet.");
            Console.ReadKey();
        }

        private void GuildStore()
        {
            bool leaveStore = false;
            while (!leaveStore)
            {
                PrintStoreMenu();
                _saveServices.SaveGame(_characterSuperModel);
                switch (GameService.ParseIntput())
                {
                    case 1:
                        ShopWeapons();
                        break;
                    case 2:
                        GameService.NewPage("No refunds! Go away!", "inv");
                        Console.ReadKey();
                        break;
                    case 3:
                        BuyPotion();
                        break;
                    case 4:
                        leaveStore = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void BuyPotion()
        {
            if(_characterSuperModel.Gold >= 10)
            {
                _characterSuperModel.Gold -= 10;
                _characterSuperModel.PotionCount++;
                Console.WriteLine("Potion added!");
            }
            else Console.WriteLine("You don't have enough money!");
            Console.ReadKey();
        }

        private void ShopWeapons()
        {
            var exit = false;
            while (exit != true)
            {
                var cat = "";
                var chosenCategory = false;
                while (!chosenCategory)
                {
                    chosenCategory = true;
                    PrintShopCategory();
                    _saveServices.SaveGame(_characterSuperModel);
                    switch (GameService.ParseIntput())
                    {
                        case 1:
                            cat = "melee";
                            break;
                        case 2:
                            cat = "ranged";
                            break;
                        case 3:
                            cat = "magic";
                            break;
                        case 4:
                            exit = true;
                            break;
                        default:
                            chosenCategory = false;
                            Console.WriteLine("Invalid input");
                            Console.ReadKey();
                            break;
                    }
                }
                if (exit == true) break;

                var canBuyItems = GetShopItems(cat);
                BuyWeapon(canBuyItems, cat);
            }
        }

        private void BuyWeapon(List<Equipment> shoppingList, string key)
        {
            var shopping = true;
            while (shopping)
            {
                PrintShopItems(shoppingList, key);
                var response = GameService.ParseIntput();
                var existingItem = shoppingList.FirstOrDefault(i => i.GearID == response);
                if (existingItem != null)
                {
                    _characterSuperModel.CharacterEquipment.Add(existingItem);
                    break;
                }
                Console.WriteLine("Invalid input");
                Console.ReadKey();
            }
            Console.WriteLine("Item bought!");
            Console.ReadKey();
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
                        if (isDead)
                        {
                            GameService.NewPage("You slowly open your eyes. \"Wha.. what happened?\"\n" +
                            "Near your bed you find a note a note. It reads:\n\n" +
                            "\"Found you beaten and bruised out by the road. What kind of champion are you trying to be? Don't do it again.\"");
                            Console.ReadKey();
                        }
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

        private List<Equipment> GetShopItems(string cat)
        {
            List<Equipment> canBuy = new List<Equipment>();
            foreach (Equipment item in _shopEquipmentList)
            {
                var existingItem = _characterSuperModel.CharacterEquipment.FirstOrDefault(i => i.GearID == item.GearID);
                if (existingItem == null)
                {
                    switch (cat)
                    {
                        case "melee":
                            if (item.GearType == GearType.Melee)
                                canBuy.Add(item);
                            break;
                        case "ranged":
                            if (item.GearType == GearType.Ranged)
                                canBuy.Add(item);
                            break;
                        case "magic":
                            if (item.GearType == GearType.Mage)
                                canBuy.Add(item);
                            break;
                    }
                }
            }
            return canBuy;
        }

        private void PrintShopItems(List<Equipment> itemList, string key)
        {
            GameService.NewPage($"\nWhich item would you like to buy?" +
                $"\n{"ID",-2}  {"Name",-18}  {"Type",-6}  {"Lvl",-4}  {"ATK+",-4}  {"HP+",-4}", key);
            foreach (Equipment item in itemList)
            {
                Console.WriteLine(item);
            }
        }

        private void PrintMenuOptions()
        {
            GameService.NewPage($"\nThe city is filled with people bustling about. You see women and children bouncing from one market stall to another.\nWhat really catches your eye is the massive arena in the center of town. At least maybe now you won't get lost again.\nOff in the distance you recognize the Combat Guild's banner waving over a few neighboring structures. You make a\nmental note to make your way there for training or a new weapon." +
                $"\n\n{GameService.GetCharacterStats(_characterSuperModel)}" +
                $"\n\nWhat to do?" +
                $"\n\n1) Visit the Combat Guild" +
                $"\n2) Fight in the Arena" +
                $"\n3) Sleep at the Inn (+{healthFromInnBed} HP)" +
                $"\n4) Open Inventory" +
                $"\n5) Leave City", "city");
        }
        private void PrintGuildMenu()
        {
            GameService.NewPage("\n1) Shop for Gear" +
                "\n2) Speak to Master Swordsman" +
                "\n3) Speak to Master Archer" +
                "\n4) Speak to Master Mage" +
                "\n5) Return to City", "guild");
        }
        private void PrintStoreMenu()
        {
            GameService.NewPage($"\nWhat would you like to do?" +
                $"\n1) Buy Items" +
                $"\n2) Sell Items" +
                $"\n3) Buy Health Potion" +
                $"\n4) Leave Shop", "shop");
        }
        private void PrintShopCategory()
        {
            GameService.NewPage("\nWhich combat style would you like to browse?" +
                        "\n1) Melee" +
                        "\n2) Ranged" +
                        "\n3) Magic" +
                        "\n4) Return to Shop", "shop");
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
