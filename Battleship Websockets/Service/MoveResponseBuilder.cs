using Battleship_Websockets.Model.Response;
using Battleship_Websockets.Model.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Battleship_Websockets.Service
{
    public class MoveResponseBuilder
    {
        private readonly(int col, int row) Cell;
        private readonly bool ShipHitted;
        private readonly ShipModel Ship;
        private readonly int BattleFieldId;

        public MoveResponseBuilder((int col, int row) cell, int battleFieldId, ShipModel ship, bool shipHitted)
        {
            Cell = cell;
            Ship = ship;
            ShipHitted = shipHitted;
            BattleFieldId = battleFieldId;
        }

        public IMoveResponse BuildMoveResponse()
        {
            IMoveResponse response;

            if (!ShipHitted)
                response = new ShipMissedResponse();
            else if (Ship == null)
                response = new ShipMissedResponse();
            else if (ShipHitted && Ship.State == ShipStatus.Damaged)
                response = new ShipDamagedResponse();
            else
                response = new ShipDestroyedResponse();

            response.BattleFieldId = BattleFieldId;
            response.Col = Cell.col;
            response.Row = Cell.row;
            
            if(response is IShipDamagedResponse)
            {
                var responseShipType = response as IShipDamagedResponse;
                responseShipType.ShipType = Ship.ShipType;

                return responseShipType;
            }

            return response;
        }
    }
}
