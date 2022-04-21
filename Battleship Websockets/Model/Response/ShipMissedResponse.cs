using Battleship_Websockets.Model.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model.Response
{
    public class ShipMissedResponse : IMoveResponse, IResponse
    {
        public int BattleFieldId { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public GameStatus GameStatus { get; set; }
        public bool ShipHitted { get; } = false;
        public ResponseTypes ResponseType { get; } = ResponseTypes.Move;
    }
}
