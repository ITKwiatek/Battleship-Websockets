using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Battleship_Websockets.Middleware
{
    public class WebSocketConnectionManager
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new();

        public ConcurrentDictionary<string, WebSocket> GetAllSockets()
        {
            return _sockets;
        }

        public string AddSocket(WebSocket socket)
        {
            string ConnId = Guid.NewGuid().ToString();
            _sockets.TryAdd(ConnId, socket);
            System.Diagnostics.Debug.WriteLine($"Connection added: {ConnId}");

            return ConnId;
        }
    }
}
