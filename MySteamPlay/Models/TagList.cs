using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MySteamPlay.Models
{
    public class TagList
    {
        public int ID { get; set; }

        public int appID { get; set; }

        public string userID { get; set; }

        public virtual List<Tag> Tags { get; set; }
    }
}