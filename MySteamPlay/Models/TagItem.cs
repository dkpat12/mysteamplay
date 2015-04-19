using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySteamPlay.Models
{
    public class TagItem
    {
        public int tagID { get; set; }      // unique identifier for the tag associated with this tag item
        public int appID { get; set; }      // unique identifier for the game associated with the library item this tag is associated with
        public ulong userID { get; set; }   // unique identifier for the user who owns the library item that this tag item is associated with
    }
}