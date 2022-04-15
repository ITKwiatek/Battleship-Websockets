using Battleship_Websockets.Model;
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

        DbSet<GameModel> Games { get; set; }
        public DbSet<BattleFieldModel> BattleFields { get; set; }
        public DbSet<PlayerModel> Players { get; set; }
        public DbSet<ShipModel> Ships { get; set; }
        public DbSet<ShipPart> ShipParts { get; set; }
        public DbSet<ShotModel> Shots { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region ManyKeys
            //PlayerGame ==> has 2 Keys
            builder.Entity<PlayerGameManyToMany>()
                .HasKey(pg => new { pg.GameId, pg.PlayerId });

            //ShipPart ==> has 2 Keys
            builder.Entity<ShipPart>()
                .HasKey(sp => new { sp.ShipId, sp.Number });
            #endregion ManyKeys

            #region Relations
            //Relations are ordered alphabetically

            //BattleField1 - Game ==> BattleField1 has one Game
            //Game - BattleField1 ==> Game has one BattleField1
            builder.Entity<BattleFieldModel>()
                .HasOne<GameModel>()
                .WithOne(g => g.BattleField1)
                .OnDelete(DeleteBehavior.Restrict);

            ////BattleField2 - Game ==> BattleField2 has one Game
            ////Game - BattleField2 ==> Game has one BattleField2
            builder.Entity<BattleFieldModel>()
                .HasOne<GameModel>()
                .WithOne(g => g.BattleField2)
                .OnDelete(DeleteBehavior.Restrict);

            //BattleField - Ship ==> BattleField has many Ships
            //Ship - BattleField ==> Ship has one BattleField
            builder.Entity<BattleFieldModel>()
                .HasMany<ShipModel>()
                .WithOne()
                .HasForeignKey(s => s.BattleFieldId);

            //BattleField - Shot ==> BattleField has many Shots
            //Shot - BattleField ==> Shot has many Battlefields
            builder.Entity<BattleFieldModel>()
                .HasMany<ShotModel>()
                .WithOne()
                .HasForeignKey(s => s.BattleFieldId);

            //Game - Player1 ==> Game has one Player1
            //Player - Game  ==> Player has many games
            builder.Entity<PlayerModel>()
                .HasMany<GameModel>()
                .WithOne(g => g.Player1)
                .OnDelete(DeleteBehavior.Restrict);

            ////Game - Player2 ==> Game has one Player2
            ////Player - Game  ==> Player has many games
            builder.Entity<PlayerModel>()
                .HasMany<GameModel>()
                .WithOne(g => g.Player2)
                .OnDelete(DeleteBehavior.Restrict);

            //Player - Shot ==> Player has many Shots
            //Shot - Player ==> Shot has one Player
            builder.Entity<PlayerModel>()
                .HasMany<ShotModel>()
                .WithOne()
                .HasForeignKey(s => s.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            //Ship - ShipPart ==> Ship has many ShipParts
            //ShipPart - Ship ==> ShipPart has one Ship
            builder.Entity<ShipModel>()
                .HasMany<ShipPart>()
                .WithOne()
                .HasForeignKey(sp => sp.ShipId);



            #endregion Relations
        }
    }
}
