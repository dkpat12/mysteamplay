using MySteamPlay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySteamPlay.Controllers
{
    public abstract class DataBaseController : Controller
    {
        protected ApplicationDbContext Database { get; set; }

        public DataBaseController()
        {
            Database = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            Database.Dispose();

            base.Dispose(disposing);
        }
    }
}