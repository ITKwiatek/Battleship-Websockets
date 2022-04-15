using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model.Ship
{
    public class ShipPart
    {
        [Key]
        public int ShipId { get; set; }
        public int Number { get; set; }
        public ShipPartStatus Status { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
    }
}
