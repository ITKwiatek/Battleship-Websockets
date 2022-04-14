using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model
{
    public class ShipModel
    {
        public int Id { get; set; }
        public int BattleFieldId { get; set; }
        public int RowBegin { get; set; }
        public int ColumnBegin { get; set; }
        public Orientation Orientation { get; set; }
        public int Length { get; set; }
        public ShipStatus State { get; set; }
    }
}
