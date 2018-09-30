using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CharacterSuperModel
    {
        public int CharacterID { get; set; }
        public string CharacterName { get; set; }
        public string CurrentLocation { get; set; }
        public int CharacterBaseHealth { get; set; }
        public int CharacterMaxHealth { get; set; }
        public int CharacterHealth { get; set; }
        public int CharacterLevel { get; set; }
        public List<Equipment> CharacterEquipment { get; set; }
        public CharacterSuperModel()
        {
            CharacterEquipment = new List<Equipment>()
            {
                new Equipment() { GearID = 1, GearName = "Bronze Sword", GearType = GearType.Melee, LvToUse = 10, BonusATK = 3, BonusHP = 0 },
            };
        }
    }
}