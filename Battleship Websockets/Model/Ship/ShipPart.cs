using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model.Ship
{
    public class ShipPart
    {
        public ShipPart(int number, int rowNumber, int columnNumber, ShipModel ship)
        {
            Number = number;
            RowNumber = rowNumber;
            ColumnNumber = columnNumber;
            Ship = ship;
        }
        public ShipPart()
        {

        }

        [Key]
        public int Id { get; set; }
        public int ShipModelId { get; set; }
        public int Number { get; set; }
        public ShipPartStatus Status { get; set; } = ShipPartStatus.New;
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        [NotMapped]
        public ShipModel Ship { get; set; }
    }
}
