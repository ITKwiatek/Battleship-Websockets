using Battleship_Websockets.Data;
using Battleship_Websockets.Model;
using Battleship_Websockets.Model.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Service
{
    public class GameCreatorService
    {
        private readonly int _player1Id;
        private readonly int _player2Id;
        private readonly string _connId;
        private readonly PlayersTurn _playerTurn;
        private readonly int _rows;
        private readonly int _columns;
        private List<ShipInitialDto> ships = new();
        private BattleFieldModel battleField1;
        private BattleFieldModel battleField2;
        private GameModel _game;
        private readonly GameDBContext _db;


        public GameCreatorService(string connId, GamePropertiesDto gameProperties)
        {
            _connId = connId;
            _player1Id = gameProperties.Player1Id;
            _player2Id = gameProperties.Player2Id;
            _playerTurn = gameProperties.FirstTurn;
            _columns = gameProperties.BattleFieldColumns;
            _rows = gameProperties.BattleFieldRows;
            ships = gameProperties.Ships;
            _db = new GameDBContext();
        }

        public GameModel CreateGame()
        {
            _game = new GameModel(_connId, _playerTurn, _player1Id, _player2Id, _rows, _columns);

            battleField1 = CrateBattleField(_game.Id, _player1Id);
            battleField2 = CrateBattleField(_game.Id, _player2Id);

            _game.BattleField1 = battleField1;
            _game.BattleField2 = battleField2;
            _db.SaveGame(_game);

            PlaceShipsOnMap(battleField1);
            PlaceShipsOnMap(battleField2);
            return _game;
        }

        public BattleFieldModel CrateBattleField(int gameId, int playerId)
        {
            var battleField = new BattleFieldModel(gameId, playerId, _rows, _columns);

            return battleField;
        }

        private void PlaceShipsOnMap(BattleFieldModel battleField)
        {
            if (ships.Count <= 0)
                return;
            ships = ships.OrderByDescending(s => s.ShipType).ToList();

            var service = new ShipArrangeService(battleField1);
            service.CreateShips(ships);

            service = new(battleField2);
            var createdShips = service.CreateShips(ships);

            createdShips.ForEach(s =>
            {
                s.BattleFieldId = battleField.Id;
            });

            _db.SaveShips(createdShips);
        }
    }
}
