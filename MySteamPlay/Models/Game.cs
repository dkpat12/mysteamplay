using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySteamPlay.Models
{
    public class Game
    {
        public int appID { get; set; }              // should be unique identifier for each game, consistent variable naming with Steam API
        public string name { get; set; }            // game name, the variable is named consistently with the Steam API
        public string img_icon_url { get; set; }    // game icon link
        public string img_logo_url { get; set; }    // game logo link

    }

    /*
     * After this bracket in HW8, we had:
     *  public class MovieDBContext : DbContext
        {
            public DbSet<Movie> Movies { get; set; }
        }
     * 
     * so perhaps similar code should go here for Game, like so?
     *  public class GameDBContext : DbContext
     *  {
     *      public DbSet<Game> Games { get; set; }
     *  }
     * */
}