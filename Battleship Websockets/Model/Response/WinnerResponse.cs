using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model.Response
{
    public class WinnerResponse : IResponse
    {
        public GameStatus GameStatus { get; set; }
        public PlayersTurn ActionReceiver { get; set; }
    }
}
