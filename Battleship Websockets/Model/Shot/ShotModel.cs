using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model
{
    public class ShotModel
    {
        public ShotModel(int battleFieldId, int rowNumber, int columnNumber)
        {
            BattleFieldId = battleFieldId;
            RowNumber = rowNumber;
            ColumnNumber = columnNumber;
            MoveTime = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public int BattleFieldId { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public DateTime MoveTime { get; }
    }
}
