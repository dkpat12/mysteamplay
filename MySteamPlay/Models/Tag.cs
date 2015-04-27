using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySteamPlay.Models
{
    public class Tag
    {
        [Key]
        public int tagID { get; set; }      // should be unique identifier for each tag

        public string tagName { get; set; }     // a description for a game, e.g. multiplayer, the genre, etc.

        public virtual TagList MyProperty { get; set; }
    }
}