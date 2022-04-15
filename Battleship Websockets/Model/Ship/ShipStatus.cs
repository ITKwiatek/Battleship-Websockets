using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model.Ship
{
    /**
     * New - Ship was not hit
     * Damaged - Ship was hit but still lives
     * Destroyed - Ship lost his lives
     **/

    public enum ShipPartStatus
    {
        New, Destroyed
    }

    public enum ShipStatus
    {
        New = ShipPartStatus.New,
        Destroyed = ShipPartStatus.Destroyed,
        Damaged
    }
}
