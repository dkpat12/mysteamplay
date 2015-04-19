using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySteamPlay.Models
{
    public class LibraryItem
    {
        public ulong steamID { get; set; }          // foreign key from User, part of composite key
        public int appID { get; set; }              // foreign key from Game, part of composite key
        //public List tagList { get; set; }         // list of indexes of tags
        public int playtime_forever { get; set; }   // all the time the owner has ever spent playing this game
        public string userComments { get; set; }    // comments by the owner
        public bool visible { get; set; }           // if true, game appears in owner's master list
        public int sortOrder { get; set; }          // the index that the owner has sorted the game into
    }

    /*
     * not sure if DBcontext will be used to create the database, but in case it is done similarly to HW8:
     *  public class LibraryItemDBContext : DbContext
     *  {
     *      public DbSet<LibraryItem> Library { get; set; }
     *  }
     * */
}