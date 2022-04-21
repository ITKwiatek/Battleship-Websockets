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
    public class CommandService
    {
        private const string Connect = "ConnId:";
        private const string Start = "Start";
        private const string Move = "Move";

        public dynamic RunCommand(string command, dynamic message, string connId)
        {
            IResponse response = new ServerErrorResponse();
            switch (command)
            {
                case Connect:
                    response = new ConnectResponse(connId);
                    break;
                case Start:
                    var gameProperties = Newtonsoft.Json.JsonConvert.DeserializeObject<GamePropertiesDto>(message.ToString());
                    var gameCreator = new GameCreatorService(connId, gameProperties) ;
                    var game = gameCreator.CreateGame();
                    response = new GameStartResponse(game.BattleField1.Id, game.BattleField2.Id, gameProperties.FirstTurn);
                    break;
                case Move:
                    int bfId = Int32.Parse(message.ToString());
                    var move = new MoveMessageDto(bfId);
                    var moveService = new MoveService(connId, move);
                    response = moveService.NextMove();
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine($"Command: {command} not known");
                    break;
            }

            return response;
        }
    }
}
