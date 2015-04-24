using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySteamPlay.Models
{
    public class GameList
    {
        public int ID { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual List<Game> GamesInList { get; set; }
    }
}