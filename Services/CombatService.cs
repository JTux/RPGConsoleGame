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


        public CharacterSuperModel Fight(CharacterSuperModel characterSuperModel, NPCS opponentList, int opponentID)
        {
            _characterSuperModel = characterSuperModel;
            _nPCS = opponentList;



            return _characterSuperModel;
        }
    }
}
