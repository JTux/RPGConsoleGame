using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class ExploringServices
    {
        private Random _rand;
        private CharacterSuperModel _characterSuperModel;
        private CombatService _combatService;

        public ExploringServices(Random rand)
        {
            _rand = rand;
        }
        public ExploringServices(Random rand, CharacterSuperModel characterSuperModel)
        {
            _rand = rand;
            _characterSuperModel = characterSuperModel;
            _combatService = new CombatService(_characterSuperModel);
        }

        public bool Commute()
        {
            var eventChance = GetChance();
            if (eventChance < 30) NegativeCommute();
            else if (eventChance < 85) NeutralCommute();
            else PositiveCommute();
            return true;
        }

        public bool Explore()
        {
            var eventChance = GetChance();
            if (eventChance < 40) NegativeEvent();
            else if (eventChance < 65) NeutralEvent();
            else PositiveEvent();
            return true;
        }

        private void NegativeEvent()
        {
            _combatService.EncounterFight(_characterSuperModel);
        }

        private void NeutralEvent()
        {
            GameService.NewPage("You found nothing and decided to head back.");
        }

        private void PositiveEvent()
        {
            var newRand = _rand.Next(1, 4);
            GameService.NewPage($"While exploring you found {newRand} Gold!");
            _characterSuperModel.Gold += newRand;
        }

        private void NegativeCommute()
        {
            _combatService.EncounterFight(_characterSuperModel);
            if (_characterSuperModel.IsDead) Console.WriteLine("oops");
        }

        private void NeutralCommute()
        {
            GameService.NewPage("Your commute went entirely uninterrupted.");
        }

        private void PositiveCommute()
        {
            var newRand = _rand.Next(1, 2);
            GameService.NewPage($"While exploring you found {newRand} Gold!");
            _characterSuperModel.Gold += newRand;
        }

        private int GetChance()
        {
            int newChance = _rand.Next(100);
            return newChance;
        }
    }
}
