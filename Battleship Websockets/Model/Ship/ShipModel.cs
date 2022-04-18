using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model.Ship
{
    public class ShipModel
    {
        public ShipModel(){}
        public ShipModel(ShipTypes type, Orientation orientation)
        {
            Orientation = orientation;
            ShipType = type;
        }
        [Key]
        public int Id { get; set; }
        public int BattleFieldId { get; set; }
        //public BattleFieldModel BattleField { get; set; }
        public int RowBegin { get; set; }
        public int ColumnBegin { get; set; }
        public Orientation Orientation { get; set; }
        public ShipStatus State { get; set; }
        public List<ShipPart> ShipParts { get; set; }
        public ShipTypes ShipType
        {
            get; private set;
        }
        public int Length
        {
            get; internal set;
        }

        public void SetShipType(ShipTypes type)
        {
            switch(type)
            {
                case ShipTypes.Small:
                    this.Length = 1;
                    break;
                case ShipTypes.Medium:
                    this.Length = 2;
                    break;
                case ShipTypes.Large:
                    this.Length = 3;
                    break;
}
            ShipType = type;
        }
    }
}
