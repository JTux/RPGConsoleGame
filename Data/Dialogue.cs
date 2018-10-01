using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Dialogue
    {
        Dictionary<string, string> dialogueDictionary = new Dictionary<string, string>();

        public Dialogue()
        {
            Populate();
        }

        public string GetDialogue(string key)
        {
            return dialogueDictionary[key];
        }

        private void Populate()
        {
            dialogueDictionary.Add("key", "dialogue");
        }

        public string StartText(string playerName)
        {
            return ($"\nAh, {playerName}. About time you got here. You'll want to be a bit more punctual henceforth.\nNow that you're finally here, shall we get started? Good." +
                    $"\n\nAs you should be well aware, you have been chosen to train in the way of combat under the village elder. \nYou will go on to represent our village in nearby city's regional tournaments. Hopefully you'll last \nlonger than our last \"champion\" did." +
                    $"\n\nBut that's neither here nor there. Let's not focus on the past. From now on your goal is to train as much \nas possible and prove you're no fool. Can you do that? I sure hope so... for your sake and mine." +
                    $"\n\nYour main goal is competing in the arena. You know the one. Massive building in the middle of the city? \nHard to miss. Well, when you get to the city you'll see it." +
                    $"\n\nDuring this process you'll find yourself on the road a lot. You can still visit your home but try not to spend \ntoo much time loitering around. You have places to be. That said if you ever have questions, your new Master \nwill always be here in the village." +
                    $"\n\nAny questions? No? Good. If I were you I'd got talk to your new Master. He probably would like to have a few words \nwith you. Otherwise if you're feeling particularly brave or stupid you can take to the road now. \nThe decision is yours.");
        }
    }
}
