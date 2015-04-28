﻿using System;
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
using Newtonsoft.Json;
using PortableSteam;

namespace MySteamPlay.Controllers
{
    public class GameListController : DataBaseController
    {
        // GET: GameLists
        public ActionResult Index()
        {
            string currentUserID = User.Identity.GetUserId();

            //Returned data must fit into GameListViewModel
            var GameQuery = from gdesc in Database.GDescriptions
                            join game in Database.Games on gdesc.appID equals game.appID
                            where gdesc.userId == currentUserID
                            select new GameListViewModel {
                                Name = game.name, 
                                LogoUrl = game.img_logo_url,
                                IconUrl = game.img_icon_url, 
                                Playtime = gdesc.playtime_forever, 
                                UserId = gdesc.userId,
                                Comment = gdesc.userComments
                            };

            return View(GameQuery);
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

            SteamWebAPI.SetGlobalKey(Security.apiKey);

            var identity = SteamIdentity.FromSteamID(Int64.Parse(providerKey));

            var response = SteamWebAPI.General().IPlayerService().GetOwnedGames(identity).IncludeAppInfo().GetResponse();            

            foreach(var res in response.Data.Games)
            {
                TimeSpan timeSpan = res.PlayTimeTotal;

                int totalHours = (int)timeSpan.TotalHours;

                GameDescrip gameDesc = new GameDescrip()
                {
                    appID = res.AppID,
                    playtime_forever = totalHours,
                    userId = currentUserID,
                    visible = true //True by default for now. 
                };

                Game game = new Game();

                if (res.Name != null)
                {
                    game.name = res.Name;
                    game.appID = res.AppID;
                    game.img_header_url = "http://cdn.akamai.steamstatic.com/steam/apps/" + res.AppID + "/header.jpg";
                    game.img_icon_url = "http://media.steampowered.com/steamcommunity/public/images/apps/" + res.AppID + "/" + res.IconUrl + ".jpg";
                    game.img_logo_url = "http://media.steampowered.com/steamcommunity/public/images/apps/" + res.AppID + "/" + res.LogoUrl + ".jpg";
                }
                else
                {
                    game.name = res.AppID.ToString();
                    game.appID = res.AppID;
                    game.img_header_url = "http://cdn.akamai.steamstatic.com/steam/apps/" + res.AppID + "/header.jpg";
                    game.img_icon_url = "http://media.steampowered.com/steamcommunity/public/images/apps/" + res.AppID + "/" + res.IconUrl + ".jpg";
                    game.img_logo_url = "http://media.steampowered.com/steamcommunity/public/images/apps/" + res.AppID + "/" + res.LogoUrl + ".jpg";
                }
                
                gameDesc.Game = game;

                Database.Games.Add(game);
                Database.GDescriptions.Add(gameDesc);
            }
            Database.SaveChanges();
           
            return RedirectToAction("Index");
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
