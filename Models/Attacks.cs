using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum AtkType { Melee = 1, Ranged, Mage, }

     public class Attacks
    {

        public int ATKID { get; set; }
        public string ATKName { get; set; }
        public int DMG { get; set; }
        public int LVToUSE { get; set; }
        public AtkType TypeOfAtk { get; set; }
        public bool Stun { get; set; }
        public bool DamageOverTime { get; set; }
        public bool Stealth { get; set; }

        public List<Attacks> GetAttacks()
        {

            List<Attacks> listOfATKS = new List<Attacks>();

            listOfATKS.Add(new Attacks() { ATKID = 1, ATKName = "Pierce", TypeOfAtk = AtkType.Melee, DMG = 3, LVToUSE = 1 });
            listOfATKS.Add(new Attacks() { ATKID = 2 , ATKName = "Blinding Rage", TypeOfAtk = AtkType.Melee, DMG = 4 , LVToUSE = 5 });
            listOfATKS.Add(new Attacks() { ATKID = 3 , ATKName = "Ambush", TypeOfAtk = AtkType.Melee, DMG = 5 , LVToUSE = 10 });
            listOfATKS.Add(new Attacks() { ATKID = 4 , ATKName = "Double Daggers", TypeOfAtk = AtkType.Melee, DMG = 6 , LVToUSE = 15 });
            listOfATKS.Add(new Attacks() { ATKID = 5 , ATKName = "Leap", TypeOfAtk = AtkType.Melee, DMG = 7 , LVToUSE = 20, Stun = true});
            listOfATKS.Add(new Attacks() { ATKID = 6 , ATKName = "Takedown", TypeOfAtk = AtkType.Melee, DMG = 8 , LVToUSE = 25 });
            listOfATKS.Add(new Attacks() { ATKID = 7 , ATKName = "Poison Shiv", TypeOfAtk = AtkType.Melee, DMG = 9 , LVToUSE = 30 , DamageOverTime  = true});
            listOfATKS.Add(new Attacks() { ATKID = 8 , ATKName = "Shadow Assault", TypeOfAtk = AtkType.Melee, DMG = 10 , LVToUSE = 35, Stealth = true });


            listOfATKS.Add(new Attacks() { ATKID = 11, ATKName = "Volley", TypeOfAtk = AtkType.Ranged, DMG = 3, LVToUSE = 1 });
            listOfATKS.Add(new Attacks() { ATKID = 12, ATKName = "Rapid Fire", TypeOfAtk = AtkType.Ranged, DMG = 4, LVToUSE = 5 });
            listOfATKS.Add(new Attacks() { ATKID = 13, ATKName = "Hail of Arrows", TypeOfAtk = AtkType.Ranged, DMG = 5, LVToUSE = 10 });
            listOfATKS.Add(new Attacks() { ATKID = 14, ATKName = "Trueshot", TypeOfAtk = AtkType.Ranged, DMG = 6, LVToUSE = 15 });
            listOfATKS.Add(new Attacks() { ATKID = 15, ATKName = "Quickdraw", TypeOfAtk = AtkType.Ranged, DMG = 7, LVToUSE = 20 });
            listOfATKS.Add(new Attacks() { ATKID = 16, ATKName = "Piercing Arrow", TypeOfAtk = AtkType.Ranged, DMG = 8, LVToUSE = 25, Stun = true });
            listOfATKS.Add(new Attacks() { ATKID = 17, ATKName = "Toxic Shot", TypeOfAtk = AtkType.Ranged, DMG = 9, LVToUSE = 30,  DamageOverTime = true });
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
