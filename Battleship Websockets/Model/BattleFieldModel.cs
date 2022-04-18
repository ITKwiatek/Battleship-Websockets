using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model
{
    public class BattleFieldModel
    {
        public BattleFieldModel(GameModel game, int playerId, int rows, int columns)
        {
            GameId = game.Id;
            //Game = game;
            PlayerId = playerId;
            Rows = rows;
            Columns = columns;
        }

        public BattleFieldModel()
        {
             
        }

        [Key]
        public int Id { get; set; }
        //public int GameId { get; set; }
        public int PlayerId { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public int GameId { get; set; }
        //public PlayerModel Player { get; set; }
    }
}
