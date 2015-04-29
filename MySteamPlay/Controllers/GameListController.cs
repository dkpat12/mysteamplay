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
using Newtonsoft.Json;
using PortableSteam;

namespace MySteamPlay.Controllers
{
    public class GameListController : DataBaseController
    {
        // GET: GameLists
        public async Task<ActionResult> Index()
        {
            // TODO Error Handling for when user is not logged in
            string currentUserID = User.Identity.GetUserId();

            //Returned data must fit into GameListViewModel
            var GameQuery = from gdesc in Database.GDescriptions
                            join game in Database.Games on gdesc.appID equals game.appID
                            join taglist in Database.TagLists on gdesc.Tags.ID equals taglist.ID
                            where gdesc.userId == currentUserID
                            select new GameListViewModel
                            {
                                Name = game.name,
                                LogoUrl = game.img_logo_url,
                                IconUrl = game.img_icon_url,
                                HeaderUrl = game.img_header_url,
                                Playtime = gdesc.playtime_forever,
                                Comment = gdesc.userComments,
                                UserId = gdesc.userId,
                                AppId = game.appID,
                                Visible = gdesc.visible,
                                GameTags = taglist,
                                AllTags = Database.Tags.ToList()
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

            // TODO Error Handling for when user is not logged in
            string currentUserID = User.Identity.GetUserId();

            Game selectedGame = await Database.Games.FindAsync(id);
            if (selectedGame == null)
            {
                return HttpNotFound();
            }
            var currTagList = selectedGame.GameDescriptions.Where(i => i.userId == currentUserID).FirstOrDefault().Tags;

            List<int> tags = new List<int>();

            if (currTagList != null)
            {
                foreach (var item in currTagList.Tags)
                {
                    tags.Add(item.tagID);
                }
            }

            var GameQuery = from gdesc in Database.GDescriptions
                            join game in Database.Games on gdesc.appID equals game.appID
                            join taglist in Database.TagLists on gdesc.Tags.ID equals taglist.ID
                            where gdesc.userId == currentUserID &&
                                  gdesc.appID == id
                            select new EditGameListViewModel
                            {
                                Name = game.name,
                                LogoUrl = game.img_logo_url,
                                IconUrl = game.img_icon_url,
                                HeaderUrl = game.img_header_url,
                                Playtime = gdesc.playtime_forever,
                                Comment = gdesc.userComments,
                                UserId = gdesc.userId,
                                AppId = game.appID,
                                GameDescId = gdesc.ID,
                                Visible = gdesc.visible,
                                AllTags = Database.Tags.ToList(),
                                GameTagIds = tags
                            };

            EditGameListViewModel foundGame = GameQuery.Single();

            return View(foundGame);
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

            //JSON response from Steam 
            var response = SteamWebAPI.General().IPlayerService().GetOwnedGames(identity).IncludeAppInfo().GetResponse();

            //Iterate through each item and add it to database
            foreach (var res in response.Data.Games)
            {
                TimeSpan timeSpan = res.PlayTimeTotal;

                int totalHours = (int)timeSpan.TotalHours;

                TagList tagList = new TagList()
                {
                    appID = res.AppID,
                    userID = currentUserID
                };

                GameDescrip gameDesc = new GameDescrip()
                {
                    appID = res.AppID,
                    playtime_forever = totalHours,
                    userId = currentUserID,
                    visible = true, //True by default for now.
                    Tags = tagList
                };

                Game game = new Game();

                if (res.Name != null)
                {
                    game.name = res.Name;
                }
                else
                {
                    game.name = res.AppID.ToString();
                }
                game.appID = res.AppID;
                game.img_header_url = "http://cdn.akamai.steamstatic.com/steam/apps/" + res.AppID + "/header.jpg";
                game.img_icon_url = "http://media.steampowered.com/steamcommunity/public/images/apps/" + res.AppID + "/" + res.IconUrl + ".jpg";
                game.img_logo_url = "http://media.steampowered.com/steamcommunity/public/images/apps/" + res.AppID + "/" + res.LogoUrl + ".jpg";

                //Ensure User entry for game doesn't exist in table
                bool doesLibraryExist = (Database.GDescriptions.Any(u => u.userId.Equals(gameDesc.userId)) &&
                    Database.GDescriptions.Any(a => a.appID.Equals(game.appID))); //AppID
                //Ensure Game doesn't already exist in game table
                bool doesGameExist = Database.Games.Any(a => a.appID.Equals(game.appID));

                if (doesLibraryExist)
                {
                    // Do nothing
                }
                else
                {
                    if (doesGameExist)
                    {
                        //Add existing game object
                        gameDesc.Game = Database.Games.Where(a => a.appID == game.appID).SingleOrDefault();
                    }
                    else
                    {
                        //add newly created game object
                        gameDesc.Game = game;
                        Database.Games.Add(game);
                    }
                    //Add User Record for game
                    Database.GDescriptions.Add(gameDesc);
                }

            }
            currentUser.GameCount = response.Data.GameCount;
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
            Game selectedGame = await Database.Games.FindAsync(id);

            if (selectedGame == null)
            {
                return HttpNotFound();
            }

            string currentUserID = User.Identity.GetUserId();

            var currTagList = selectedGame.GameDescriptions.Where(i => i.userId == currentUserID).FirstOrDefault().Tags;

            List<int> tags = new List<int>();

            if (currTagList != null)
            {
                foreach (var item in currTagList.Tags)
                {
                    tags.Add(item.tagID);
                }
            }
            var GameQuery = from gdesc in Database.GDescriptions
                            join game in Database.Games on gdesc.appID equals game.appID
                            join taglist in Database.TagLists on gdesc.Tags.ID equals taglist.ID
                            where gdesc.userId == currentUserID &&
                                  gdesc.appID == id
                            select new EditGameListViewModel
                            {
                                Name = game.name,
                                LogoUrl = game.img_logo_url,
                                IconUrl = game.img_icon_url,
                                HeaderUrl = game.img_header_url,
                                Playtime = gdesc.playtime_forever,
                                Comment = gdesc.userComments,
                                UserId = gdesc.userId,
                                AppId = game.appID,
                                GameDescId = gdesc.ID,
                                Visible = gdesc.visible,
                                AllTags = Database.Tags.ToList(),
                                GameTagIds = tags
                            };

            EditGameListViewModel foundGame = GameQuery.Single();

            return View(foundGame);
        }

        // POST: GameLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Details(EditGameListViewModel editGame)
        {
            if (ModelState.IsValid)
            {
                string currentUserID = User.Identity.GetUserId();
                GameDescrip editedGame = Database.GDescriptions.Where(x => x.userId == currentUserID && x.appID == editGame.AppId).Single();
                editedGame.userComments = editGame.Comment;

                List<Tag> tags = new List<Tag>();

                foreach (var item in editGame.GameTagIds)
                {
                    tags.Add(Database.Tags.Find(item));
                }

                TagList taglist = new TagList()
                {
                    appID = editGame.AppId,
                    userID = currentUserID,
                    Tags = tags
                };

                editedGame.Tags = taglist;

                Database.Entry(editedGame).State = EntityState.Modified;
                await Database.SaveChangesAsync();

                return RedirectToAction("Details");
            }
            return View(editGame);
        }
        //public ActionResult Edit([Bind(Include = "Comment")] GameListViewModel editGame)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Database.Entry(editGame).State = EntityState.Modified;
        //        Database.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(editGame);
        //}

        //// GET: GameLists/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    GameList gameList = await Database.GLists.FindAsync(id);
        //    if (gameList == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(gameList);
        //}

        //// POST: GameLists/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    GameList gameList = await Database.GLists.FindAsync(id);
        //    Database.GLists.Remove(gameList);
        //    await Database.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
    }
}
