using Battleship_Websockets.Data;
using Battleship_Websockets.Model;
using Battleship_Websockets.Model.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Service
{
    public class GameStateService
    {
        private int battleFieldId;
        private readonly GameDBContext _db;
        public GameStateService(int battleFieldId)
        {
            this.battleFieldId = battleFieldId;
            _db = new GameDBContext();
        }

        public GameStatus CheckGameStatus()
        {
            if(areAllShipsDestroyed())
            {
                return CheckWinner();
            }

            return GameStatus.Open;
        }

        private GameStatus CheckWinner()
        {
            var bf = _db.GetBattleFieldById(battleFieldId);
            var game = _db.GetGameByBattleFieldId(battleFieldId);
            if (game.BattleField1 == bf)
                return GameStatus.Player2Won;
            else if (game.BattleField2 == bf)
                return GameStatus.Player1Won;
            else
                return GameStatus.Open;
        }

        private bool areAllShipsDestroyed()
        {
            var shipParts = _db.GetShipPartsByBattleFieldIdAndState(battleFieldId, ShipPartStatus.New);

            if(shipParts.Count == 0)
            {
                return true;
            }

            return false; ;
        }
    }
}
