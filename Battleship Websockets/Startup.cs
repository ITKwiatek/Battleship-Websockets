using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;


namespace Battleship_Websockets
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWebSockets();

            app.Use(async (context, next) =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    System.Diagnostics.Debug.WriteLine("is WS");
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    System.Diagnostics.Debug.WriteLine("Connected");

                    await ReceiveMessage(webSocket, async (result, buffer) =>
                    {
                        if(result.MessageType == WebSocketMessageType.Text)
                        {
                            System.Diagnostics.Debug.WriteLine("Received message");
                        }
                        else if(result.MessageType == WebSocketMessageType.Close)
                        {
                            System.Diagnostics.Debug.WriteLine("Received close message");
                            return;
                        }
                    });
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("not WS");
                    await next();
                }
            });

            app.Run(async context =>
            {
                System.Diagnostics.Debug.WriteLine("3rd request delegate");
            });
        }

        private async Task ReceiveMessage(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];
            while(socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                    cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
        //public void WriteRequestParam(HttpContext context)
        //{
        //    System.Diagnostics.Debug.WriteLine("Re");
        //}




            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //});
        //}

    }
}
