using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Data
{
    public class GameDBContext : DbContext
    {
        private readonly string _connectionString;
        public GameDBContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public GameDBContext(DbContextOptions<GameDBContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}
