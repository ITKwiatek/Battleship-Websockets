using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model.Ship
{
    public interface ShipModel
    {
        [Key]
        public int Id { get; set; }
        public int BattleFieldId { get; set; }
        public int RowBegin { get; set; }
        public int ColumnBegin { get; set; }
        public Orientation Orientation { get; set; }
        public ShipTypes ShipType { get; set; }
        public ShipStatus State { get; set; }
    }
}
