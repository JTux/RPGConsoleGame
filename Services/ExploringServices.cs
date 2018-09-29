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

        public ExploringServices(Random rand)
        {
            _rand = rand;
        }

        public void Commute()
        {
            var eventChance = GetChance();
            if (eventChance < 30) NegativeEvent();
            else if (eventChance < 85) NeutralEvent();
            else PositiveEvent();
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
        }

        private void NeutralEvent()
        {
            GameService.NewPage("Neutral Event");
        }

        private void PositiveEvent()
        {
            GameService.NewPage("Good Event");
        }

        private int GetChance()
        {
            int newChance = _rand.Next(100);
            return newChance;
        }
    }
}
