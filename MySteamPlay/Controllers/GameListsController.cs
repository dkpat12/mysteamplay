using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MySteamPlay.Models;
using Microsoft.AspNet.Identity;

namespace MySteamPlay.Controllers
{
    public class GameListsController : DataBaseController
    {
        // GET: GameLists
        public async Task<ActionResult> Index()
        {
            return View(await Database.GLists.ToListAsync());
        }

        // GET: GameLists/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameList gameList = await Database.GLists.FindAsync(id);
            if (gameList == null)
            {
                return HttpNotFound();
            }
            return View(gameList);
        }

        // GET: GameLists/Create
        public ActionResult Create()
        {
            string currentUserID = User.Identity.GetUserId();
            ApplicationUser currentUser = Database.Users.Find(currentUserID);
            string providerKey = currentUser.Logins.First().ProviderKey;

            providerKey = providerKey.Substring(providerKey.Length - 17);

            string json;
            using (var client = new WebClient())
            {
                
                json = client.DownloadString("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=AF8461EDF5817463B5E69E013DBF84AB&steamid="+providerKey+"&format=json");
            }

            string test = json;
            return null;
        }

        // POST: GameLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID")] GameList gameList)
        {
            if (ModelState.IsValid)
            {
                Database.GLists.Add(gameList);
                await Database.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(gameList);
        }

        // GET: GameLists/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameList gameList = await Database.GLists.FindAsync(id);
            if (gameList == null)
            {
                return HttpNotFound();
            }
            return View(gameList);
        }

        // POST: GameLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID")] GameList gameList)
        {
            if (ModelState.IsValid)
            {
                Database.Entry(gameList).State = EntityState.Modified;
                await Database.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(gameList);
        }

        // GET: GameLists/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameList gameList = await Database.GLists.FindAsync(id);
            if (gameList == null)
            {
                return HttpNotFound();
            }
            return View(gameList);
        }

        // POST: GameLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            GameList gameList = await Database.GLists.FindAsync(id);
            Database.GLists.Remove(gameList);
            await Database.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
