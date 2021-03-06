﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum StyleType { Melee = 1, Ranged, Mage }

    public class CharacterSuperModel
    {
        public int CharacterID { get; set; }
        public string CharacterName { get; set; }
        public string CurrentLocation { get; set; }
        public int CharacterLevel { get; set; }
        public int CharacterBaseHealth { get; set; }
        public int CharacterMaxHealth { get; set; }
        public int CharacterHealth { get; set; }
        public bool IsDead { get; set; }
        public int ArenaWins { get; set; }
        public int Gold { get; set; }
        public int PotionCount { get; set; }
        public StyleType CombatStyle { get; set; }
        public List<Equipment> CharacterEquipment { get; set; }
        public List<Attacks> CharacterAttacks { get; set; }

      
        public CharacterSuperModel()
        {
            CharacterEquipment = new List<Equipment>();
            CharacterAttacks = new List<Attacks>();
        }
    }
}