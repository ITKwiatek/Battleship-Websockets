using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model.Ship
{
    public class ShipInitialDto
    {
        public ShipTypes ShipType { get; set; }
        public int ShipCount { get; set; }
        public Orientation Orientation { get; set; }
    }
}
