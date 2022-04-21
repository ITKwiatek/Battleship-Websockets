using Battleship_Websockets.Model.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model.Response
{
    interface IShipDamagedResponse : IMoveResponse
    {
        public ShipStatus ShipStatus { get; }
        public ShipTypes ShipType { get; set; }
    }
}
