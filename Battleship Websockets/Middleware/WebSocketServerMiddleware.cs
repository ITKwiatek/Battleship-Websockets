using Battleship_Websockets.Data;
using Battleship_Websockets.Service;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Battleship_Websockets.Model.Response;

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
            CommandService commandService = new();
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string ConnId = _manager.AddSocket(webSocket);
                await SendConnIdAsync(webSocket, ConnId);

                await ReceiveMessage(webSocket, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var JSON = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        var received = JsonConvert.DeserializeObject<dynamic>(JSON);
                        string command = received.Command.ToString();
                        string from = received.From.ToString();
                        var message = JsonConvert.DeserializeObject<dynamic>(received.Message.ToString());
                        var response = commandService.RunCommand(command, message, ConnId);
                        await RouteJSONMessageAsync(from, response);
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

        public async Task RouteJSONMessageAsync(string sendTo, dynamic obj)
        {
            if(Guid.TryParse(sendTo, out Guid guidOutput))
            {
                var sock = _manager.GetAllSockets().FirstOrDefault(s => s.Key == sendTo);

                if (sock.Value != null)
                {
                    if (sock.Value.State == WebSocketState.Open)
                    {
                       await sock.Value.SendAsync(Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(obj)),
                       WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Invalid Recipient");
                    }
                }
            }
        }

        private async Task SendConnIdAsync(WebSocket socket, string connId)
        {
            var connResponse = new ConnectResponse(connId);
            var buffer = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(connResponse));

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
