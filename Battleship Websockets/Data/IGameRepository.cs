using Battleship_Websockets.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Data
{
    public interface IGameRepository
    {
        public bool SaveGame(GameModel model);
    }
}
