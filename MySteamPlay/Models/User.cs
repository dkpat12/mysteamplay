using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySteamPlay.Models
{
    public class User
    {
        public ulong steamID { get; set; }      // unique identifier for this user's Steam account
        public string personaName { get; set; } // after reviewing Steam API documentation, still not sure what this is
        public string avatar { get; set; }      // ditto
        public string email { get; set; }       // user's email, not pulled from Steam API
        // in the meeting notes, there is also an attribute called "identity2?"
    }
}