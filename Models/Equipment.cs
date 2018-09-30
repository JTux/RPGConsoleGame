using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{

    public enum GearType { Melee =1, Ranged, Mage }

    public class Equipment
    {
        public int GearID { get; set; }
        public string GearName { get; set; }
        public GearType GearType { get; set; }
        public int LvToUse { get; set; }
        public int BonusHP { get; set; }
        public int BonusATK { get; set; }

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
    }
}
