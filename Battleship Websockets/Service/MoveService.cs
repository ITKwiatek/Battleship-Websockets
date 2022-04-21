using Battleship_Websockets.Data;
using Battleship_Websockets.Model;
using Battleship_Websockets.Model.Response;
using Battleship_Websockets.Model.Ship;
using Battleship_Websockets.Model.Shot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Service
{
    public class MoveService
    {
        private string connId;
        private MoveMessageDto move;
        private readonly List<ShotModel> _oldShots;
        private readonly BattleFieldModel _battleField;
        private readonly GameDBContext _db;

        public MoveService(string connId, MoveMessageDto move)
        {
            _db = new GameDBContext();
            this.connId = connId;
            this.move = move;
            _oldShots = GetOldShots();
            _battleField = GetBattleField();
        }



        public IMoveResponse NextMove()
        {
            (int col, int row) cell = RandomizeAvailableCell();
            SaveShot(cell);
            (bool shipHitted, ShipModel ship) = TryShootTheShip(cell);

            var gameStateService = new GameStateService(_battleField.Id);

            var responseBuilder = new MoveResponseBuilder(cell, _battleField.Id, ship, shipHitted);

            var response = responseBuilder.BuildMoveResponse();
            response.GameStatus = gameStateService.CheckGameStatus();

            return response;
        }

        private (bool shipHited, ShipModel ship) TryShootTheShip((int col, int row) cell)
        {      
            var shipPart = _db.GetShipPartByCellAndBattleField(cell, _battleField.Id);
            
            bool shipHited = DestroyShipPart(shipPart);

            if (!shipHited)
                return (shipHited, null);

            DamageTheShip(shipPart);

            return (shipHited, shipPart.Ship);
        }

        private ShipStatus DamageTheShip(ShipPart shipPart)
        {          
            var ship = shipPart.Ship;
            var destroyedShipParts = _db.GetShipPartsByShipIdAndState(ship.Id, ShipPartStatus.Destroyed);
            if (ship.Length == destroyedShipParts.Count)
                ship.State = ShipStatus.Destroyed;
            else
                ship.State = ShipStatus.Damaged;

            _db.UpdateShip(ship);

            return ship.State;
        }

        private bool DestroyShipPart(ShipPart shipPart)
        {
            if (shipPart == null)
                return false;

            shipPart.Status = ShipPartStatus.Destroyed;

            bool updated = _db.UpdateShipPart(shipPart);

            return updated;
        }

        private (int,int) RandomizeAvailableCell()
        {
            (int col, int row) cell = RandomizeMove();
            if (!isCellAvailable(cell))
            {
                cell = RandomizeAvailableCell();
            }

            return cell;
        }

        private List<ShotModel> GetOldShots()
        {        
            var shotsDone = _db.GetShotsByBattleFieldId(move.BattleFieldId);
            if (shotsDone == null)
                return new List<ShotModel>();

            return shotsDone;
        }

        private BattleFieldModel GetBattleField()
        {           
            var battleField = _db.GetBattleFieldById(move.BattleFieldId);

            return battleField;
        }

        private (int, int) RandomizeMove()
        {
            var rand = new Random();

            int col = rand.Next(0, _battleField.Columns);
            int row = rand.Next(0, _battleField.Rows);

            return (col, row);
        }

        private bool isCellAvailable((int col, int row) cell)
        {
            foreach(var shot in _oldShots)
            {
                var shootedCell = (shot.ColumnNumber, shot.RowNumber);
                if (shootedCell == cell)
                    return false;
            };

            return true;
        }

        private void SaveShot((int col, int row) cell)
        {
            var shot = new ShotModel(_battleField.Id, cell.row, cell.col);
            _db.SaveShot(shot);
        }
    }
}
