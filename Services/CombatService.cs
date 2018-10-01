using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class CombatService
    {
        private CharacterSuperModel _characterSuperModel;
        private NPCS _nPCS = new NPCS();
        private Equipment _equipment;
        private Attacks _attacks;
        private List<NPCS> _NpcList;
        private List<Equipment> _EquipmentList;
        private List<Attacks> _AttackList;
        private NPCS currentFlight;
        private Random rnd = new Random();

        public CombatService(CharacterSuperModel character)
        {
            _EquipmentList = character.CharacterEquipment;
            _AttackList = character.CharacterAttacks;
        }

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
            _NpcList = _nPCS.GetNPCS();
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
                GameService.NewPage($"\n{EnemyCurrentStatus.NPCName}\n" +
                    $"Hp:{EnemyCurrentStatus.HP}/{enemy.HP}\n\n\n\n\n" +
                    $"{CurrentStatus.CharacterName}\n" +
                    $"Hp: {CurrentStatus.CharacterHealth}/{CurrentStatus.CharacterMaxHealth}\n" +
                    "[1. Attack]\n" +
                    "[2. Drink Hp Pot]: " + (CurrentStatus.PotionCount) + " Remaining \n" +
                    "[3. Run]\n"
                    ,"battle");
                char response = Console.ReadKey().KeyChar;

                switch (response)
                {
                    case '1':
                        {
                            EnemyCurrentStatus.HP -= YourAtkResult(DisplayAndPickAtkOptions(_characterSuperModel.CharacterAttacks).DMG);
                            break;
                        }
                    case '2':
                        {
                            if (CurrentStatus.PotionCount > 0)
                            {
                                CurrentStatus.PotionCount--;
                                if (CurrentStatus.CharacterHealth != CurrentStatus.CharacterMaxHealth)
                                {
                                    if ((CurrentStatus.CharacterHealth + 10) <= CurrentStatus.CharacterMaxHealth)
                                        CurrentStatus.CharacterHealth += 10;
                                    else CurrentStatus.CharacterHealth = CurrentStatus.CharacterMaxHealth;
                                    Console.WriteLine("You Feel Better.");
                                }
                                else
                                {
                                    Console.WriteLine("You're already at max health.");
                                    goto Repeat;
                                }
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("You are out of Potions!");
                                Console.ReadLine();
                                goto Repeat;
                            }
                            break;
                        }
                    case '3':
                        {
                            Console.WriteLine("You Ran...");
                            Console.ReadLine();
                            goto EndFight;
                        }
                    default:
                        {
                            goto Repeat;
                        }
                }
                if (enemy.HP >= 1) { EnemyAtk(enemy.ATK); }
            }
            if (_characterSuperModel.CharacterHealth > 0)
            {
                _characterSuperModel.CharacterLevel++;
                _characterSuperModel.CharacterBaseHealth += 5;
                _characterSuperModel.CharacterMaxHealth += 5;
                _characterSuperModel.CharacterHealth = _characterSuperModel.CharacterMaxHealth;
                _characterSuperModel.Gold += ((enemy.ATK) * 2);
            }
            else { _characterSuperModel.IsDead = true; }
            EndFight:;
        }

        private void EnemyAtk(int enemyAtk)
        {
            int chance = rnd.Next(1, 100);
            if (chance > 15)
            {
                Console.WriteLine("You are hit for "+(enemyAtk)+"!!!");
                _characterSuperModel.CharacterHealth -= enemyAtk;
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("You dodge the attack!!!");
                Console.ReadLine();
            }
        }

        private int YourAtkResult(int yourAtk)
        {
            int chance = rnd.Next(1, 100);
            if (chance > 30)
            {
                Console.WriteLine("You hit for " + (yourAtk) + " attack!!!");
                Console.ReadLine();
                return yourAtk;
            }
            else if (chance > 10 && chance <= 30)
            {
                Console.WriteLine("The Enemy dodged the attack!!!");
                Console.ReadLine();
                return 0;
            }
            else
            {
                Console.WriteLine("You Crit for " + (yourAtk * 2) + "Damage");
                Console.ReadLine();
                return (yourAtk * 2);
            }
        }

        private void grabAtkOptions()
        {
            var list = _characterSuperModel.CharacterAttacks;
        }
        private Attacks DisplayAndPickAtkOptions(List<Attacks> attacks)
        {

            List<Attacks> attacksLocal = new List<Attacks>();

            string b = _characterSuperModel.CombatStyle.ToString();

        TopList:
            int i = 0;
            Console.Clear();
            Console.WriteLine("Choose An Atk:");
            foreach (var attack in attacks)
            {

                string a = attack.TypeOfAtk.ToString();
                if (a == b)
                {
                    i++;
                    attacksLocal.Add(attack);
                    Console.WriteLine(i + ") " + attack.ATKName);
                }
            }
            var resp = (Console.ReadKey().KeyChar).ToString();
            Console.Clear();
            if (int.TryParse(resp, out int respInt))
            {
                if (respInt < (attacksLocal.Count + 1) && respInt > 0)
                {
                    return attacksLocal.ElementAt((respInt - 1));
                }
                else { goto TopList; }
            }
            else { goto TopList; }
        }
    }
}
