using Battleship_Websockets.Model.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model.Response
{
    public class ShipDestroyedResponse : IMoveResponse, IShipDamagedResponse, IResponse
    {
        public int BattleFieldId { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public GameStatus GameStatus { get; set; }
        public bool ShipHitted { get; } = true;
        public ShipStatus ShipStatus { get; set; } = ShipStatus.Destroyed;
        public ResponseTypes ResponseType { get; } = ResponseTypes.Move;
        public ShipTypes ShipType { get; set; }
    }
}
