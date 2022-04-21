using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model.Shot
{
    public class MoveMessageDto
    {
        public MoveMessageDto(int battleFieldId)
        {
            BattleFieldId = battleFieldId;
        }

        public int BattleFieldId { get; set; }
    }
}
