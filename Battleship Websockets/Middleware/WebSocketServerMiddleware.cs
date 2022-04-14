using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Battleship_Websockets.Middleware
{
    public class WebSocketServerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly WebSocketConnectionManager _manager;
        public WebSocketServerMiddleware(RequestDelegate next, WebSocketConnectionManager manager)
        {
            _next = next;
            _manager = manager;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                System.Diagnostics.Debug.WriteLine("is WS");
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                System.Diagnostics.Debug.WriteLine("Connected");

                string ConnId = _manager.AddSocket(webSocket);
                await SendConnIdAsync(webSocket, ConnId);

                await ReceiveMessage(webSocket, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        System.Diagnostics.Debug.WriteLine($"Received message {Encoding.UTF8.GetString(buffer, 0, result.Count)}");
                        await RouteJSONMessageAsync(Encoding.UTF8.GetString(buffer, 0, result.Count));
                        return;
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        System.Diagnostics.Debug.WriteLine("Received close message");
                        return;
                    }
                });
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("not WS");
                await _next(context);
            }
        }

        public async Task RouteJSONMessageAsync(string message)
        {
            var routeOb = JsonConvert.DeserializeObject<dynamic>(message);

            if(Guid.TryParse(routeOb.From.ToString(), out Guid guidOutput))
            {
                System.Diagnostics.Debug.WriteLine("Targeted");
                var sock = _manager.GetAllSockets().FirstOrDefault(s => s.Key == routeOb.From.ToString());

                if (sock.Value != null)
                {
                    if (sock.Value.State == WebSocketState.Open)
                    {
                       await sock.Value.SendAsync(Encoding.UTF8.GetBytes(routeOb.Message.ToString() + " Response for " + routeOb.From),
                       WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Invalid Recipient");
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Broadcast");
                foreach(var sock in _manager.GetAllSockets())
                {
                    if(sock.Value.State == WebSocketState.Open)
                    {
                        await sock.Value.SendAsync(Encoding.UTF8.GetBytes(routeOb.Message.ToString() + " Response"),
                        WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
            }
        }

        private async Task SendConnIdAsync(WebSocket socket, string connId)
        {
            var buffer = Encoding.UTF8.GetBytes("ConnId: " + connId);

            System.Diagnostics.Debug.WriteLine("Sending back...");
            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        private async Task ReceiveMessage(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];
            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                    cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
    }
}
