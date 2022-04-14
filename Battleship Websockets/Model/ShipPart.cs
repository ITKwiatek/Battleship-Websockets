using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model
{
    public class ShipPart
    {
        public int Id { get; set; }
        public ShipPartStatus Status { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
