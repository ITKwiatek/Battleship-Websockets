using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model
{
    public class PlayerGameManyToMany
    {
        [Key]
        public int PlayerId { get; set; }
        [Key]
        public int GameId { get; set; }
        //public PlayerModel Player { get; set; }
        //public GameModel Game { get; set; }
    }
}
