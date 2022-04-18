using Battleship_Websockets.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Data
{
    public class GameRepository : IGameRepository
    {
        private GameDBContext _db;
        public GameRepository(GameDBContext db)
        {
            _db = db;
        }

        public bool SaveGame(GameModel model)
        {
            _db.Add(model);
            _db.SaveChanges();
            return true;
        }
    }
}
