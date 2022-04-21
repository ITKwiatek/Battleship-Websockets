using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model.Response
{
    public interface IMoveResponse : IResponse
    {
        public int BattleFieldId { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public GameStatus GameStatus { get; set; }
        public bool ShipHitted { get; }
        public ResponseTypes ResponseType { get; }
    }
}
