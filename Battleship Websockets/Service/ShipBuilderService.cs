using Battleship_Websockets.Model;
using Battleship_Websockets.Model.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Service
{
    public static class ShipBuilderService
    {
        public static ShipModel CreateShipType(ShipInitialDto shipDto)
        {
            ShipModel ship = new ShipModel();
            switch(shipDto.ShipType)
            {
                case ShipTypes.Large:
                    ship.Orientation = Orientation.Horizontal;
                    ship.SetShipType(ShipTypes.Large);
                    break;
                case ShipTypes.Medium:
                    ship.Orientation = Orientation.Vertical;
                    ship.SetShipType(ShipTypes.Medium);
                    break;
                case ShipTypes.Small:
                    ship.Orientation = Orientation.Horizontal;
                    ship.SetShipType(ShipTypes.Small);
                    break;
            }

            return ship;
        }
    }
}
