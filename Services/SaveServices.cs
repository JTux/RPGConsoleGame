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

            characterFile.Write($"GameID: {superModel.CharacterID},");
            characterFile.Write($"CharacterName: {superModel.CharacterName},");
            characterFile.Write($"CurrentLocation: {superModel.CurrentLocation},");
            characterFile.Write($"CharacterBaseHealth: {superModel.CharacterBaseHealth},");
            characterFile.Write($"CharacterMaxHealth: {superModel.CharacterMaxHealth},");
            characterFile.Write($"CharacterHealth: {superModel.CharacterHealth},");

            characterFile.Close();
            UpdateSettings();
        }


        //public string CharacterName { get; set; }
        //public string CurrentLocation { get; set; }
        //public int CharacterBaseHealth { get; set; }
        //public int CharacterMaxHealth { get; set; }
        //public int CharacterHealth { get; set; }


        public void LoadSave()
        {
            CreateDirectories();
            UpdateSettings();
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
