using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model.Shot
{
    public class MoveMessage
    {
        public int PlayerId { get; set; }
        public int BattleFieldId { get; set; }
    }
}
