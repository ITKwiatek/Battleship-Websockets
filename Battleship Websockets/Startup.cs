using Battleship_Websockets.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.EntityFrameworkCore;


using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Battleship_Websockets.Data;

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
            services.AddWebSocketManager();

            services.AddDbContext<GameDBContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IGameRepository, GameRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWebSockets();

            app.UseWebSocketServer();

            //app.Run(async context =>
            //{
            //    System.Diagnostics.Debug.WriteLine("3rd request delegate");
            //});
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
