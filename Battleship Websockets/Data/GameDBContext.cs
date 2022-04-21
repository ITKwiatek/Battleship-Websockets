using Battleship_Websockets.Model;
using Battleship_Websockets.Model.Ship;
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
        public GameDBContext()
        {

        }

        public GameDBContext(DbContextOptions<GameDBContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,5435;Database=Battleship;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        public DbSet<BattleFieldModel> BattleFields { get; set; }
        public DbSet<GameModel> Games { get; set; }
        public DbSet<PlayerModel> Players { get; set; }
        public DbSet<ShipModel> Ships { get; set; }
        public DbSet<ShipPart> ShipParts { get; set; }
        public DbSet<ShotModel> Shots { get; set; }

        #region BattleField
        public BattleFieldModel GetBattleFieldById(int id)
        {
            var bf = BattleFields.FirstOrDefault(b => b.Id == id);

            return bf;
        }

        public bool SaveBattleField(BattleFieldModel battleField)
        {
            BattleFields.Add(battleField);
            SaveChanges();
            return true;
        }
        #endregion

        #region Game
        public GameModel GetGameByBattleFieldId(int bfId)
        {
            var game = Games.FirstOrDefault(g => g.BattleField1.Id == bfId);
            if(game is null)
                game = Games.FirstOrDefault(g => g.BattleField2.Id == bfId);

            return game;
        }
        public bool SaveGame(GameModel game)
        {
            Games.Add(game);
            SaveChanges();

            return true;
        }
        #endregion

        #region Ship
        public ShipModel GetShipById(int id)
        {
            var ship = Ships.FirstOrDefault(sp => sp.Id == id);
            return ship;
        }

        public bool UpdateShip(ShipModel ship)
        {
            Ships.Update(ship);
            SaveChanges();

            return true;
        }

        public bool SaveShips(List<ShipModel> ships)
        {
            Ships.AddRange(ships);
            SaveChanges();

            foreach(var ship in ships)
            {
                SaveShipParts(ship.ShipParts);
            }
            return true;
        }
        #endregion

        #region ShipPart
        public ShipPart GetShipPartByCellAndBattleField((int col, int row)cell, int battleFieldId)
        {
            var shipPart = ShipParts.FirstOrDefault(sp => sp.Ship.BattleFieldId == battleFieldId && sp.ColumnNumber == cell.col && sp.RowNumber == cell.row);
            if (shipPart != null)
                shipPart.Ship = Ships.FirstOrDefault(s => s.Id == shipPart.ShipModelId);

            return shipPart;
        }
        public List<ShipPart> GetShipPartsByBattleFieldIdAndState(int battleFieldId, ShipPartStatus state)
        {

            var shipParts = ShipParts.Where(sp => sp.Ship.BattleFieldId == battleFieldId && sp.Status == state).ToList();

            if (shipParts is null)
                return new List<ShipPart>();

            return shipParts;
        }

        public List<ShipPart> GetShipPartsByShipIdAndState(int shipId, ShipPartStatus state)
        {
            var shipParts = ShipParts.Where(sp => sp.ShipModelId == shipId && sp.Status == state).ToList();

            if (shipParts is null)
                return new List<ShipPart>();

            return shipParts;
        }

        public bool UpdateShipPart(ShipPart shipPart)
        {
            ShipParts.Update(shipPart);
            SaveChanges();
            return true;
        }

        public bool SaveShipParts(List<ShipPart> shipParts)
        {
            ShipParts.AddRange(shipParts);
            SaveChanges();
            return true;
        }
        #endregion

        #region Shots
        public bool SaveShot(ShotModel shot)
        {
            Shots.Add(shot);
            SaveChanges();

            return true;
        }
        public List<ShotModel> GetShotsByBattleFieldId(int battleFieldId)
        {
            List<ShotModel> shots = Shots.Where(s => s.BattleFieldId == battleFieldId).ToList();

            if (shots is null)
                return new List<ShotModel>();

            return shots;
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Relations

            //BattleField - Shot ==> BattleField has many Shots
            //Shot - BattleField ==> Shot has many Battlefields
            builder.Entity<BattleFieldModel>()
                .HasMany<ShotModel>()
                .WithOne()
                .HasForeignKey(s => s.BattleFieldId);

            //Ship - ShipPart ==> Ship has many ShipParts
            //ShipPart - Ship ==> ShipPart has one Ship
            builder.Entity<ShipModel>()
                .HasMany<ShipPart>()
                .WithOne(s => s.Ship)
                .HasForeignKey(s => s.ShipModelId);

            #endregion Relations
        }
    }
}
