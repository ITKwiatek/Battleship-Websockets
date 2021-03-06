using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model
{
    public class GameModel
    {
        public GameModel()
        {

        }
        public GameModel(string connId, PlayersTurn playerTurn, int player1Id, int player2Id, int rows, int columns)
        {
            ConnId = connId;
            PlayerTurn = playerTurn;
            Player1Id = player1Id;
            Player2Id = player2Id;

            StartTime = DateTime.Now;
            Status = GameStatus.Open;
        }

        [Key]
        public int Id { get; set; }
        public string ConnId { get; set; }
        public DateTime StartTime { get; set; }
        public PlayersTurn PlayerTurn { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public GameStatus Status { get; set; }
        public BattleFieldModel BattleField1 { get; set; }
        public BattleFieldModel BattleField2 { get; set; }
    }
}
