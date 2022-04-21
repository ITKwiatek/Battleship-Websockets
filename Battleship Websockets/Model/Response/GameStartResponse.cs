using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model.Response
{
    public class GameStartResponse : IResponse
    {
        public GameStartResponse(int battleField1Id, int battleField2Id, PlayersTurn playerTurn)
        {
            BattleField1Id = battleField1Id;
            BattleField2Id = battleField2Id;
            PlayerTurn = playerTurn;
        }
        public ResponseTypes ResponseType { get; } = ResponseTypes.Start;
        public int BattleField1Id { get; }
        public int BattleField2Id { get; }
        public PlayersTurn PlayerTurn { get; }
    }
}
