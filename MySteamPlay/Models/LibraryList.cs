using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySteamPlay.Models
{
    public static class LibraryList
    {
        public static List<LibraryItem> GetLibraryItems(int BlockNumber, int BlockSize)
        {
            int startIndex = (BlockNumber - 1) * BlockSize;

            var filePath = AppDomain.CurrentDomain.BaseDirectory + "";
            var doc = XDocument.Load(filePath);

            var LibraryItems = (from g in doc.Descendants("libraryItem")
                                select new LibraryItem
                                {
                                    steamID = g.Attributes("id").Single().Value,
                                    appID = g.Element("appId").Value,
                                    playtime_forever = g.Element("playtimeForever").Value,
                                    userComments = g.Element("userComments").Value,
                                }).Skip(startIndex).Take(BlockSize).ToList();

            return LibraryItems;
        }
    }
}