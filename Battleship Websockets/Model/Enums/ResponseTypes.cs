using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model.Response
{
    public enum ResponseTypes
    {
        Move, ConnectionId, Start, ServerError, Connected
    }
}
