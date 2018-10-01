using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CombatService
    {
        private CharacterSuperModel _characterSuperModel;
        private NPCS _nPCS;
        private Equipment _equipment;
        private Attacks _attacks;
        private List<NPCS> _NpcList;
        private List<Equipment> _EquipmentList;
        private List<Attacks> _AttackList;
        private NPCS currentFlight;
        private Random rnd = new Random();

        public CharacterSuperModel EncounterFight(CharacterSuperModel characterSuperModel)
        {
            _characterSuperModel = characterSuperModel;
            List<NPCS> NpcList = _nPCS.GetNPCS();
            int IDLOOKUP = (_characterSuperModel.CharacterLevel);
            int RandomNPCID = rnd.Next(1, (IDLOOKUP / 2) + 1);

            LookupFightByNPCID(RandomNPCID);

            Fight(_characterSuperModel, currentFlight);

            return _characterSuperModel;
        }

        public CharacterSuperModel ArenaFight(CharacterSuperModel characterSuperModel)
        {
            _characterSuperModel = characterSuperModel;
            _NpcList = _nPCS.GetNPCS();

            int IDLOOKUP = ((_characterSuperModel.ArenaWins) + 15);

            if (IDLOOKUP < 21)
            {
                LookupFightByNPCID(IDLOOKUP);
                Fight(_characterSuperModel, currentFlight);
            }
            return _characterSuperModel;
        }

        private void LookupFightByNPCID(int IDLOOKUP)
        {
            foreach (NPCS nPCS in _NpcList)
            {
                if (nPCS.NPCID == IDLOOKUP)
                {
                    currentFlight = nPCS;
                }
            }
        }

        private void Fight(CharacterSuperModel u, NPCS enemy)
        {
            CharacterSuperModel CurrentStatus = u;
            NPCS EnemyCurrentStatus = enemy;
            int yourATK = 0;

            while (CurrentStatus.CharacterHealth > 0 && EnemyCurrentStatus.HP > 0)
            {
                Repeat:
                Console.Clear();
                Console.WriteLine($"{EnemyCurrentStatus.NPCName}\n" +
                    $"Hp:{EnemyCurrentStatus.HP}/{enemy.HP}\n\n\n\n\n" +
                    $"{CurrentStatus.CharacterName}\n" +
                    $"Hp: {CurrentStatus.CharacterHealth}/{CurrentStatus.CharacterMaxHealth}\n" +
                    "[1. Attack]\n" +
                    "[2.Drink Hp Pot]: 1 Remaining \n" +
                    "[3. Run]\n"
                    );
                char response = Console.ReadKey().KeyChar;

                switch (response)
                {
                    case '1':
                        {
                            Console.WriteLine();

                            EnemyCurrentStatus.HP -= YourAtkResult(yourATK);
                            break;
                        }
                    case '2':
                        {
                            break;
                        }
                    case '3':
                        {
                            break;
                        }
                    default:
                        {
                            goto Repeat;
                        }

                }
                //YourAtk(Curre);
                if (enemy.HP >= 1){ EnemyAtk(enemy.ATK); }
               
                

                   
            }
            if (_characterSuperModel.CharacterHealth > 0)
            {
                _characterSuperModel.CharacterLevel++;
                _characterSuperModel.Gold += ((enemy.ATK) * 2);
            }
            else { _characterSuperModel.IsDead = true; }
        }

        private void EnemyAtk(int enemyAtk)
        {
            int chance = rnd.Next(1, 100);
            if (chance > 15)
            {
                _characterSuperModel.CharacterHealth -= enemyAtk;
            }
            else
            {
                Console.WriteLine("You dodge the atk!!!");
            }
        }

        private int YourAtkResult(int yourAtk)
        {
            int chance = rnd.Next(1, 100);
            if (chance > 30)
            {
                Console.WriteLine("You hit for " +(yourAtk)+" atk!!!");
                return yourAtk;
            }
            else if(chance >10 && chance <= 30) 
            {
                Console.WriteLine("The Enemy dodge the atk!!!");
                return 0;
            }
            else
            {
                Console.WriteLine("You Crit for "+ (yourAtk * (13/10)) +"Damage");
                return (yourAtk * (13 / 10));
            }
        }

        private void grabAtkOptions()
        {
        
        }

    }
}
