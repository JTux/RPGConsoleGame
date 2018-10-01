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

        public override string ToString()
        {            
            return $"{ATKID,2}) {ATKName,-20}  {TypeOfAtk,-6}  {LVToUSE,-3}  {Stun,-5}  {DamageOverTime,-6}  {Stealth,-5}";
        }
    }
}