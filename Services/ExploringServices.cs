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

        public void Commute()
        {
            var eventChance = GetChance();
            if (eventChance < 30) NegativeCommute();
            else if (eventChance < 85) NeutralCommute();
            else PositiveCommute();
        }

        public void Explore()
        {
            var eventChance = GetChance();
            if (eventChance < 40) NegativeEvent();
            else if (eventChance < 65) NeutralEvent();
            else PositiveEvent();
        }

        private void NegativeEvent()
        {
            GameService.NewPage("Bad Event");
            _combatService.EncounterFight(_characterSuperModel);
        }

        private void NeutralEvent()
        {
            GameService.NewPage("You found nothing and decided to head back.");
        }

        private void PositiveEvent()
        {
            var newRand = _rand.Next(8);
            GameService.NewPage($"While exploring you found {newRand} Gold!");
            _characterSuperModel.Gold += newRand;
        }

        private void NegativeCommute()
        {
            GameService.NewPage("Bad Commute");
            _combatService.EncounterFight(_characterSuperModel);
        }

        private void NeutralCommute()
        {
            GameService.NewPage("Your commute went entirely uninterrupted.");
        }

        private void PositiveCommute()
        {
            var newRand = _rand.Next(5);
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
