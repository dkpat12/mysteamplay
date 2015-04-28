using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MySteamPlay.Models
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int appID { get; set; }              //Unique Steam ID for every game 

        public string name { get; set; }            // game name, the variable is named consistently with the Steam API

        public string img_icon_url { get; set; }    // game icon link

        public string img_header_url { get; set; }    // game header link

        public string img_logo_url { get; set; }    // game logo link

        public virtual List<GameDescrip> GameDescriptions{ get; set; }
    }
}