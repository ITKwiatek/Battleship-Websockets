using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Model
{
    public class PlayerModel
    {
        [Key]
        public int Id { get; set; }
    }
}
