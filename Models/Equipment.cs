﻿using System;
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

        public override string ToString()
        {
            return $"{GearID}) {GearName} {GearType} {LvToUse} {BonusATK} {BonusHP}";
        }
    }
}
