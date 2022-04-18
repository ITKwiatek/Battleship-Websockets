using Battleship_Websockets.Model.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model.Response
{
    public class MoveResponse : IResponse
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public GameStatus GameStatus { get; set; }
        public ShipStatus ShipStatus { get; set; } = ShipStatus.New;
        public PlayersTurn ActionReceiver { get; set; }
    }
}
