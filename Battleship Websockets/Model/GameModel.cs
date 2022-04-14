using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model
{
    public class GameModel
    {
        public int Id { get; set; }
        public string ConnId { get; set; }
        public DateTime StartTime { get; set; }
        public PlayersTurn PlayerTurn { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public GameStatus Status { get; set; }

    }
}
