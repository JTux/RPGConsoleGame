﻿using Models;
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
            if (eventChance < 50) NegativeCommute();
            else if (eventChance < 85) NeutralCommute();
            else PositiveCommute();
            return true;
        }

        public bool Explore()
        {
            var eventChance = GetChance();
            if (eventChance < 75) NegativeEvent();
            else if (eventChance < 95) NeutralEvent();
            else PositiveEvent();
            if (_characterSuperModel.IsDead) return true;
            else return false;
        }

        private void NegativeEvent()
        {
            _combatService.EncounterFight(_characterSuperModel);
        }

        private void NeutralEvent()
        {
            GameService.NewPage("\nYou found nothing and decided to head back.", "filler");
            Console.ReadKey();
        }

        private void PositiveEvent()
        {
            var newRand = _rand.Next(1, 4);
            GameService.NewPage($"\nWhile exploring you found {newRand} Gold!", "event");
            _characterSuperModel.Gold += newRand;
            Console.ReadKey();
        }

        private void NegativeCommute()
        {
            _combatService.EncounterFight(_characterSuperModel);
        }

        private void NeutralCommute()
        {
            GameService.NewPage("\nYour commute went entirely uninterrupted.", "filler");
            Console.ReadKey();
        }

        private void PositiveCommute()
        {
            var newRand = _rand.Next(1, 2);
            GameService.NewPage($"\nWhile on your way you found {newRand} Gold!", "event");
            _characterSuperModel.Gold += newRand;
            Console.ReadKey();
        }

        private int GetChance()
        {
            int newChance = _rand.Next(100);
            return newChance;
        }
    }
}
