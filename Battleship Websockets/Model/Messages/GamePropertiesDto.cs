using Battleship_Websockets.Model.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model
{
    public class GamePropertiesDto
    {
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public int BattleFieldRows { get; set; }
        public int BattleFieldColumns { get; set; }
        public List<ShipInitialDto> Ships { get; set; } = new();
        public PlayersTurn FirstTurn { get; set; }
    }
}
