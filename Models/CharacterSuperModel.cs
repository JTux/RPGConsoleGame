using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CharacterSuperModel
    {
        public string CharacterName { get; set; }
        public string CurrentLocation { get; set; }
        public int CharacterBaseHealth { get; set; }
        public int CharacterMaxHealth { get; set; }
        public int CharacterHealth { get; set; }
    }
}
