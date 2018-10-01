using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SaveServices
    {
        public static int SaveGames { get; set; }

        public void LoadSettings()
        {
            CreateDirectories();
            if (!File.Exists($"./Files/Settings.txt"))
                CreateSettings();
            string settings = File.ReadAllText($"./Files/Settings.txt");
            string[] lines = settings.Split(',');

            foreach (string line in lines)
            {
                if (line.Contains("GameCount:"))
                {
                    var newValue = line.Substring(line.LastIndexOf(' ') + 1);
                    SaveGames = int.Parse(newValue);
                }
            }
            UpdateSettings();
        }

        private void CreateDirectories()
        {
            if (!Directory.Exists($"./Files"))
                Directory.CreateDirectory($"./Files");
            if (!Directory.Exists($"./Files/Saves"))
                Directory.CreateDirectory($"./Files/Saves");
        }

        private void CreateSettings()
        {
            StreamWriter settings = new StreamWriter($"./Files/Settings.txt");
            settings.Close();
        }

        public void UpdateSettings()
        {
            StreamWriter settings = new StreamWriter($"./Files/Settings.txt");
            settings.Write($"GameCount: {SaveGames},");
            settings.Close();
        }

        public void SaveGame(CharacterSuperModel superModel)
        {
            StreamWriter characterFile = new StreamWriter($"./Files/Saves/Game{superModel.CharacterID}.txt");

            characterFile.Write($"CharacterID: {superModel.CharacterID},");
            characterFile.Write($"CharacterName: {superModel.CharacterName},");
            characterFile.Write($"CurrentLocation: {superModel.CurrentLocation},");
            characterFile.Write($"CharacterBaseHealth: {superModel.CharacterBaseHealth},");
            characterFile.Write($"CharacterMaxHealth: {superModel.CharacterMaxHealth},");
            characterFile.Write($"CharacterHealth: {superModel.CharacterHealth},");
            characterFile.Write($"PotionCount: {superModel.PotionCount},");
            characterFile.Write($"Gold: {superModel.Gold},");
            characterFile.Write($"CharacterLevel: {superModel.CharacterLevel},");
            characterFile.Write($"CombatStyle: {superModel.CombatStyle},");
            characterFile.Write($"IsDead: {superModel.IsDead},");

            var itemIDList = "";
            superModel.CharacterEquipment = superModel.CharacterEquipment.OrderBy(i => i.GearID).ToList();
            foreach (Equipment item in superModel.CharacterEquipment)
            {
                itemIDList += $"{item.GearID};";
            }
            characterFile.Write($"CharacterItems: {itemIDList},");

            var attackIDList = "";
            superModel.CharacterAttacks = superModel.CharacterAttacks.OrderBy(i => i.ATKID).ToList();
            foreach (Attacks attack in superModel.CharacterAttacks)
            {
                attackIDList += $"{attack.ATKID};";
            }
            characterFile.Write($"CharacterAttacks: {attackIDList},");

            characterFile.Close();
            UpdateSettings();
        }

        public void PrintSaves()
        {
            CharacterSuperModel loadedSuperModel = new CharacterSuperModel();
            CreateDirectories();
            for (int i = 1; i <= SaveGames; i++)
            {
                if (File.Exists($"./Files/Saves/Game{i}.txt"))
                {
                    string saveGameTraits = File.ReadAllText($"./Files/Saves/Game{i}.txt");
                    string[] loadedTraits = saveGameTraits.Split(',');

                    foreach (string trait in loadedTraits)
                    {
                        if (trait.Contains("CharacterID:"))
                        {
                            var loadID = trait.Substring(trait.IndexOf(' ') + 1);
                            loadedSuperModel.CharacterID = int.Parse(loadID);
                        }
                        else if (trait.Contains("CharacterName:"))
                        {
                            var loadCharacterName = trait.Substring(trait.IndexOf(' ') + 1);
                            loadedSuperModel.CharacterName = loadCharacterName;
                        }
                        else if (trait.Contains("CharacterLevel:"))
                        {
                            var loadCharacterLevel = trait.Substring(trait.IndexOf(' ') + 1);
                            loadedSuperModel.CharacterLevel = int.Parse(loadCharacterLevel);
                        }
                        else if (trait.Contains("CurrentLocation:"))
                        {
                            var loadCharacterLocation = trait.Substring(trait.IndexOf(' ') + 1);
                            loadedSuperModel.CurrentLocation = loadCharacterLocation;
                        }
                        else if (trait.Contains("CombatStyle:"))
                        {
                            var loadCombatStyle = trait.Substring(trait.IndexOf(' ') + 1);
                            switch (loadCombatStyle.ToLower())
                            {
                                case "melee":
                                    loadedSuperModel.CombatStyle = StyleType.Melee;
                                    break;
                                case "ranged":
                                    loadedSuperModel.CombatStyle = StyleType.Ranged;
                                    break;
                                case "mage":
                                    loadedSuperModel.CombatStyle = StyleType.Mage;
                                    break;
                                default:
                                    loadedSuperModel.CombatStyle = StyleType.Melee;
                                    break;
                            }
                        }
                    }
                    Console.WriteLine($"{loadedSuperModel.CharacterID}) {loadedSuperModel.CharacterName}, a level {loadedSuperModel.CharacterLevel} practicing {loadedSuperModel.CombatStyle} currently in the {loadedSuperModel.CurrentLocation}.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"{i}) Error loading save file.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            Console.WriteLine($"{SaveGames + 1}) Return to Menu");
        }

        public CharacterSuperModel LoadSave(int saveID)
        {
            CharacterSuperModel loadedSuperModel = new CharacterSuperModel();
            CreateDirectories();
            for (int i = 1; i <= SaveGames; i++)
            {
                if (File.Exists($"./Files/Saves/Game{i}.txt"))
                {
                    string saveGameTraits = File.ReadAllText($"./Files/Saves/Game{i}.txt");
                    string[] loadedTraits = saveGameTraits.Split(',');

                    foreach (string trait in loadedTraits)
                    {
                        if (trait.Contains("CharacterID:"))
                        {
                            var loadID = trait.Substring(trait.IndexOf(' ') + 1);
                            loadedSuperModel.CharacterID = int.Parse(loadID);
                        }
                        else if (trait.Contains("IsDead:"))
                        {
                            var loadIsDead = trait.Substring(trait.IndexOf(' ') + 1);
                            if (loadIsDead == "False") loadedSuperModel.IsDead = false;
                            else if (loadIsDead == "True") loadedSuperModel.IsDead = true;
                        }
                        else if (trait.Contains("CharacterName:"))
                        {
                            var loadCharacterName = trait.Substring(trait.IndexOf(' ') + 1);
                            loadedSuperModel.CharacterName = loadCharacterName;
                        }
                        else if (trait.Contains("CurrentLocation:"))
                        {
                            var loadCharacterLocation = trait.Substring(trait.IndexOf(' ') + 1);
                            loadedSuperModel.CurrentLocation = loadCharacterLocation;
                        }
                        else if (trait.Contains("CharacterBaseHealth:"))
                        {
                            var loadBaseHealth = trait.Substring(trait.IndexOf(' ') + 1);
                            loadedSuperModel.CharacterBaseHealth = int.Parse(loadBaseHealth);
                        }
                        else if (trait.Contains("CharacterMaxHealth:"))
                        {
                            var loadMaxHealth = trait.Substring(trait.IndexOf(' ') + 1);
                            loadedSuperModel.CharacterMaxHealth = int.Parse(loadMaxHealth);
                        }
                        else if (trait.Contains("CharacterHealth:"))
                        {
                            var loadHealth = trait.Substring(trait.IndexOf(' ') + 1);
                            loadedSuperModel.CharacterHealth = int.Parse(loadHealth);
                        }
                        else if (trait.Contains("CharacterLevel:"))
                        {
                            var loadLevel = trait.Substring(trait.IndexOf(' ') + 1);
                            loadedSuperModel.CharacterLevel = int.Parse(loadLevel);
                        }
                        else if (trait.Contains("PotionCount:"))
                        {
                            var loadPotionCount = trait.Substring(trait.IndexOf(' ') + 1);
                            loadedSuperModel.PotionCount = int.Parse(loadPotionCount);
                        }
                        else if (trait.Contains("Gold:"))
                        {
                            var loadGold = trait.Substring(trait.IndexOf(' ') + 1);
                            loadedSuperModel.Gold = int.Parse(loadGold);
                        }
                        else if (trait.Contains("CombatStyle:"))
                        {
                            var loadCombatStyle = trait.Substring(trait.IndexOf(' ') + 1);
                            switch (loadCombatStyle.ToLower())
                            {
                                case "melee":
                                    loadedSuperModel.CombatStyle = StyleType.Melee;
                                    break;
                                case "ranged":
                                    loadedSuperModel.CombatStyle = StyleType.Ranged;
                                    break;
                                case "mage":
                                    loadedSuperModel.CombatStyle = StyleType.Mage;
                                    break;
                                default:
                                    loadedSuperModel.CombatStyle = StyleType.Melee;
                                    break;
                            }
                        }
                        else if (trait.Contains("CharacterItems:"))
                        {
                            var invService = new InventoryServices();
                            var getItems = invService.GetEquipment();

                            var loadItemString = trait.Substring(trait.IndexOf(' ') + 1);
                            string[] loadedItemIDs = loadItemString.Split(';');

                            foreach (string itemID in loadedItemIDs)
                            {
                                if (itemID != "")
                                {
                                    var newItem = getItems.FirstOrDefault(l => l.GearID == int.Parse(itemID));
                                    loadedSuperModel.CharacterEquipment.Add(newItem);
                                }
                            }
                        }
                        else if (trait.Contains("CharacterAttacks:"))
                        {
                            var invService = new InventoryServices();
                            var getATtacks = invService.GetAttacks();

                            var loadAttacksString = trait.Substring(trait.IndexOf(' ') + 1);
                            string[] loadedAttackIDs = loadAttacksString.Split(';');

                            foreach (var attackID in loadedAttackIDs)
                            {
                                if (attackID != "")
                                {
                                    var newAttack = getATtacks.FirstOrDefault(l => l.ATKID == int.Parse(attackID));
                                    loadedSuperModel.CharacterAttacks.Add(newAttack);
                                }
                            }
                        }
                    }
                }
                else Console.WriteLine("Game files corrupt");
                if (i == saveID) break;
            }

            return loadedSuperModel;
        }

        public void Reset()
        {
            CreateDirectories();
            for (int i = 1; i <= SaveGames; i++)
            {
                if (File.Exists($"./Files/Saves/Game{i}.txt"))
                {
                    File.Delete($"./Files/Saves/Game{i}.txt");
                }
            }
            SaveGames = 0;
            UpdateSettings();
        }
    }
}