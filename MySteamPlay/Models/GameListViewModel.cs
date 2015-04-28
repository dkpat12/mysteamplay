using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySteamPlay.Models
{
    public class GameListViewModel
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string IconUrl { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public int Playtime { get; set; }
        public int AppId { get; set; }
    }
}