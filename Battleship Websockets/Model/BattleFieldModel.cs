using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model
{
    public class BattleFieldModel
    {
        public BattleFieldModel(int gameId, int playerId, int rows, int columns)
        {
            GameId = gameId;
            PlayerId = playerId;
            Rows = rows;
            Columns = columns;
        }

        [Key]
        public int Id { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public GameModel Game { get; set; }
        public PlayerModel Player { get; set; }
    }
}
