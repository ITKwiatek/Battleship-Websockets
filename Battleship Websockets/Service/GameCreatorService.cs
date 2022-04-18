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


        public GameCreatorService(string connId, GamePropertiesDto gameProperties)
        {
            _connId = connId;
            _player1Id = gameProperties.Player1Id;
            _player2Id = gameProperties.Player2Id;
            _playerTurn = gameProperties.FirstTurn;
            _columns = gameProperties.BattleFieldColumns;
            _rows = gameProperties.BattleFieldRows;
            ships = gameProperties.Ships;
        }

        public GameModel CreateGame()
        {
            _game = new GameModel(
                _connId,
                _playerTurn,
                _player1Id,
                _player2Id,
                _rows,
                _columns
                );

            battleField1 = CrateBattleField(_game, _player1Id);
            battleField2 = CrateBattleField(_game, _player2Id);

            SaveGame();
            SaveBattleFields();
            PlaceShipsOnMap(battleField1);
            PlaceShipsOnMap(battleField2);
            return _game;
        }

        private GameModel SaveGame()
        {
            using (var db = new GameDBContext())
            {
                try
                {
                    _game.BattleField1 = battleField1;
                    _game.BattleField2 = battleField2;
                    db.Games.Add(_game);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Game saving failed... " + e.Message);
                }
            }

            return _game;
        }

        private (BattleFieldModel, BattleFieldModel) SaveBattleFields()
        {
            using (var db = new GameDBContext())
            {
                try
                {
                    battleField1.GameId = _game.Id;
                    battleField2.GameId = _game.Id;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("BattleFields saving failed... " + e.Message);
                }
            }

            return (battleField1, battleField1);
        }

        private List<ShipModel> SaveShips(List<ShipModel> createdShips)
        { 
            using (var db = new GameDBContext())
            {
                db.Ships.AddRange(createdShips);
                db.SaveChanges();
            }

            return createdShips;
        }

        public BattleFieldModel CrateBattleField(GameModel game, int playerId)
        {
            BattleFieldModel battleField = new(game, playerId, _rows, _columns);

            return battleField;
        }

        private void PlaceShipsOnMap(BattleFieldModel battleField)
        {
            if (ships.Count <= 0)
                return;
            ships = ships.OrderByDescending(s => s.ShipType).ToList();

            ShipArrangeService service = new(battleField1);
            service.CreateShips(ships);

            service = new(battleField2);
            List<ShipModel>createdShips = service.CreateShips(ships);

            createdShips.ForEach(s =>
            {
                s.BattleFieldId = battleField.Id;
            });

            SaveShips(createdShips);
        }

        private bool SaveGameAndBattleShips(GameModel game, BattleFieldModel bf1, BattleFieldModel bf2)
        {
            return true;
        }
    }
}
