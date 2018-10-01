using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class NPCS
    {
        public int NPCID { get; set; }
        public string NPCName { get; set; }
        public int HP { get; set; }
        public int ATK { get; set; }

        public List<NPCS> GetNPCS() {

            List<NPCS> listOfNPCS = new List<NPCS>();

            listOfNPCS.Add(new NPCS() { NPCID = 1, NPCName = "Goblin", ATK = 3, HP = 12 });
            listOfNPCS.Add(new NPCS() { NPCID = 2, NPCName = "Giant Spider", ATK = 3, HP = 15 });
            listOfNPCS.Add(new NPCS() { NPCID = 3, NPCName = "Thug", ATK = 4, HP = 18 });
            listOfNPCS.Add(new NPCS() { NPCID = 4, NPCName = "Dark Imp", ATK = 4, HP = 21 });
            listOfNPCS.Add(new NPCS() { NPCID = 5, NPCName = "Troll", ATK = 5, HP = 25 });
            listOfNPCS.Add(new NPCS() { NPCID = 6, NPCName = "Giant", ATK = 5, HP = 30 });
            listOfNPCS.Add(new NPCS() { NPCID = 7, NPCName = "Dark Knight", ATK = 6, HP = 34 });
            listOfNPCS.Add(new NPCS() { NPCID = 8, NPCName = "Poisonous Snake", ATK = 6, HP = 35 });
            listOfNPCS.Add(new NPCS() { NPCID = 9, NPCName = "Elf", ATK = 7, HP = 38 });
            listOfNPCS.Add(new NPCS() { NPCID = 10, NPCName = "Wizard", ATK = 7, HP = 42 });
            listOfNPCS.Add(new NPCS() { NPCID = 11, NPCName = "Blood Elf", ATK = 8, HP = 44 });
            listOfNPCS.Add(new NPCS() { NPCID = 12, NPCName = "Dark Wizard", ATK = 8, HP = 50 });
            listOfNPCS.Add(new NPCS() { NPCID = 13, NPCName = "Poisonous Siren", ATK = 9, HP = 52 });
            listOfNPCS.Add(new NPCS() { NPCID = 14, NPCName = "Wraith", ATK = 9, HP = 55 });

            listOfNPCS.Add(new NPCS() { NPCID = 15, NPCName = "Turk, the Enraged ", ATK = 12, HP = 58 });
            listOfNPCS.Add(new NPCS() { NPCID = 16, NPCName = "Drake, the Snake", ATK = 11, HP = 70 });
            listOfNPCS.Add(new NPCS() { NPCID = 17, NPCName = "Fang, The Dark Knight Leader", ATK = 12, HP = 78 });
            listOfNPCS.Add(new NPCS() { NPCID = 18, NPCName = "Rizen, The Siren's Watch", ATK = 13, HP = 84 });
            listOfNPCS.Add(new NPCS() { NPCID = 19, NPCName = "Cyclops", ATK = 14, HP = 98 });
            listOfNPCS.Add(new NPCS() { NPCID = 20, NPCName = "Oden, Elder Gorgon", ATK = 15, HP = 140 });

            return listOfNPCS;
        }

    }
}
