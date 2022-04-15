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
        private readonly int rows;
        private readonly int columns;

        public GameCreatorService(int player1Id, int player2Id, string connId, PlayersTurn playerTurn)
        {
            _player1Id = player1Id;
            _player2Id = player2Id;
            _connId = connId;
            _playerTurn = playerTurn;
        }

        public GameModel CreateGame()
        {
            var game = new GameModel(
                _connId,
                _playerTurn,
                CreatePlayer().Id,
                CreatePlayer().Id,
                rows,
                columns
                );

            return game;
        }

        public PlayerModel CreatePlayer()
        {
            PlayerModel player = new();

            return player;
        }

        public void PlaceShipsOnMap(List<ShipInitialDto> ships)
        {
            ships.OrderByDescending(s => s.ShipType);
            ships.ForEach(ship =>
            {
                //put ship on map
            });
        }
    }
}
