using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model.Response
{
    public class ConnectResponse : IResponse
    {
        public ConnectResponse(string connId)
        {
            ConnId = connId;
        }

        public string ConnId { get; set; }
        public ResponseTypes ResponseType { get; } = ResponseTypes.Connected;
    }
}
