using Battleship_Websockets.Data;
using Battleship_Websockets.Model;
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


        public CommandService()
        {
        }

        public string RunCommand(string command, dynamic message, string connId)
        {
            switch (command)
            {
                case Connect:
                    System.Diagnostics.Debug.WriteLine("Run Connectiong...");
                    break;

                case Start:
                    System.Diagnostics.Debug.WriteLine("Start Game...");
                    GamePropertiesDto gameProperties = Newtonsoft.Json.JsonConvert.DeserializeObject<GamePropertiesDto>(message.ToString());
                    GameCreatorService gameCreator = new(connId, gameProperties) ;
                    gameCreator.CreateGame();
                    break;
                case Move:
                    System.Diagnostics.Debug.WriteLine("Move...");
                    MoveMessage move = Newtonsoft.Json.JsonConvert.DeserializeObject<MoveMessage>(message.ToString());
                    var moveservice = new MoveService(connId, move);
                    moveservice.NextMove();
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine($"Command: {command} not known");
                    break;
            }

            return "Implement me";
        }
    }
}
