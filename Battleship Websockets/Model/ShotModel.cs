using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model
{
    public class ShotModel
    {
        public int Id { get; set; }
        public int BattleFieldId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int PlayerId { get; set; }
        public DateTime MoveTime { get; set; }
    }
}
