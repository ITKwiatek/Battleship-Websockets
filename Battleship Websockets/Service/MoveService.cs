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
        private MoveMessage move;
        private readonly List<ShotModel> _oldShots;
        private readonly BattleFieldModel _battleField;

        public MoveService(string connId, MoveMessage move)
        {
            this.connId = connId;
            this.move = move;
            _oldShots = GetOldShots();
            _battleField = GetBattleField();
        }



        public MoveResponse NextMove()
        {
            (int col, int row) cell = RandomizeAvailableCell();
            var shootedShipState = TryShootTheShip(cell);

            MoveResponse response = new();
            response.Col = cell.col;
            response.Row = cell.row;
            response.ShipStatus = shootedShipState;
            response.ActionReceiver(move.PlayerId);


        }

        private ShipStatus TryShootTheShip((int col, int row) cell)
        {
            ShipPart shipPart;
            using (var db = new GameDBContext())
            {
                shipPart = db.ShipParts.FirstOrDefault(sp => sp.Ship.BattleFieldId == _battleField.Id && sp.ColumnNumber == cell.col && sp.RowNumber == cell.row);
            }

            bool shipHited = DestroyShipPart(shipPart);

            if (shipHited)
                DamageTheShip(shipPart);

            return 
        }

        private ShipStatus DamageTheShip(ShipPart shipPart)
        {
            ShipModel ship;
            List<ShipPart> destroyedShipParts;
            using (var db = new GameDBContext())
            {
                ship = db.Ships.FirstOrDefault(sp => sp.Id == shipPart.Ship.Id);

                destroyedShipParts = db.ShipParts.Where(sp => sp.Ship.Id == ship.Id && sp.Status == ShipPartStatus.Destroyed).ToList();
                if (ship.Length == destroyedShipParts.Count)
                    ship.State = ShipStatus.Destroyed;
                else
                    ship.State = ShipStatus.Damaged;

                db.SaveChanges();
            }

            return ship.State;
        }

        private bool DestroyShipPart(ShipPart shipPart)
        {

            if (shipPart == null)
            {
                return false;
            }
            using (var db = new GameDBContext())
            {
                shipPart.Status = ShipPartStatus.Destroyed;

                db.SaveChanges();
            }

            return true;
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
            List<ShotModel> shotsDone;
            using (var db = new GameDBContext())
            {
                shotsDone = db.Shots.Where(s => s.BattleFieldId == move.BattleFieldId).ToList();
            }

            return shotsDone;
        }

        private BattleFieldModel GetBattleField()
        {
            BattleFieldModel battleField;
            using (var db = new GameDBContext())
            {
                battleField = db.BattleFields.FirstOrDefault(b => b.Id == move.BattleFieldId);
            }

            return battleField;
        }

        private (int, int) RandomizeMove()
        {
            Random rand = new();

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
    }
}
