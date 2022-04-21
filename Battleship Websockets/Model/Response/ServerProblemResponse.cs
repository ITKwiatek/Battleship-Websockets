using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model.Response
{
    public class ServerErrorResponse : IResponse
    {
        public ResponseTypes ResponseType { get; } = ResponseTypes.ServerError;
    }
}
