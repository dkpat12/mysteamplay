using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MySteamPlay.Models
{
    public class GameDescrip
    {
        public int ID { get; set; }

        [ForeignKey("User")]
        public string userId { get; set; }

        [ForeignKey("Game")]
        public int appID { get; set; }             // foreign key from Game, part of composite key

        public int playtime_forever { get; set; }   // all the time the owner has ever spent playing this game

        public string userComments { get; set; }    // comments by the owner

        public bool visible { get; set; }           // if true, game appears in owner's master list

        public virtual List<TagList> Tags { get; set; }

        public virtual Game Game { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    /*
     * not sure if DBcontext will be used to create the database, but in case it is done similarly to HW8:
     *  public class LibraryItemDBContext : DbContext
     *  {
     *      public DbSet<LibraryItem> Library { get; set; }
     *  }
     * */
}