using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySteamPlay.Models
{
    public class Game
    {
        public int ID { get; set; }  

        public int appID { get; set; }              // should be unique identifier for each game, consistent variable naming with Steam API
        public string name { get; set; }            // game name, the variable is named consistently with the Steam API
        public string img_icon_url { get; set; }    // game icon link
        public string img_logo_url { get; set; }    // game logo link
        public virtual List<GameDescrip> GameDescriptions{ get; set; }
    }
}