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

        public void CreateDirectories()
        {
            if (!Directory.Exists($"./Files"))
                Directory.CreateDirectory($"./Files");
            if (!Directory.Exists($"./Files/Saves"))
                Directory.CreateDirectory($"./Files/Saves");
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
                        else if (trait.Contains("CurrentLocation:"))
                        {
                            var loadCharacterLocation = trait.Substring(trait.IndexOf(' ') + 1);
                            loadedSuperModel.CurrentLocation = loadCharacterLocation;
                        }
                    }
                    Console.WriteLine($"{loadedSuperModel.CharacterID}) {loadedSuperModel.CharacterName} currently in the {loadedSuperModel.CurrentLocation}");
                }
                else Console.WriteLine("Game files corrupt");
            }
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
