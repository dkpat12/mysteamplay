namespace MySteamPlay.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MySteamPlay.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MySteamPlay.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MySteamPlay.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Tags.AddOrUpdate(
                t => t.tagName,
                new Tag { tagName = "Multiplayer" },
                new Tag { tagName = "Internet Required" },
                new Tag { tagName = "Completed" },
                new Tag { tagName = "Junk" },
                new Tag { tagName = "RPG" },
                new Tag { tagName = "ARPG" },
                new Tag { tagName = "Roguelike" },
                new Tag { tagName = "Tactical RPG"},
                new Tag { tagName = "Action"},
                new Tag { tagName = "Beat 'em Up"},
                new Tag { tagName = "Hack 'n' Slash"},
                new Tag { tagName = "Platformer"},
                new Tag { tagName = "Fighting"},
                new Tag { tagName = "Stealth"},
                new Tag { tagName = "Shooter"},
                new Tag { tagName = "Cover-Based Shooter"},
                new Tag { tagName = "FPS"},
                new Tag { tagName = "Rail Shooter"},
                new Tag { tagName = "Sidescrolling Shooter"},
                new Tag { tagName = "Shoot 'em Up"},
                new Tag { tagName = "Bullet Hell"},
                new Tag { tagName = "Tactical Shooter"},
                new Tag { tagName = "Third-Person Shooter"},
                new Tag { tagName = "Simulation"},
                new Tag { tagName = "Management Simuator"},
                new Tag { tagName = "Vehicle Simulator"},
                new Tag { tagName = "Life Simulator"},
                new Tag { tagName = "Casual"},
                new Tag { tagName = "Puzzle"},
                new Tag { tagName = "Tabletop"},
                new Tag { tagName = "Rhythm"},
                new Tag { tagName = "Adventure"},
                new Tag { tagName = "Point-and-Click"},
                new Tag { tagName = "Text-Based"},
                new Tag { tagName = "Visual Novel"},
                new Tag { tagName = "Horror"},
                new Tag { tagName = "Sandbox"},
                new Tag { tagName = "Strategy"},
                new Tag { tagName = "Tactics"},
                new Tag { tagName = "Turn-Based"},
                new Tag { tagName = "Real Time"},
                new Tag { tagName = "MMO"},
                new Tag { tagName = "Sports"},
                new Tag { tagName = "Racing"},
                new Tag { tagName = "Character Action"},
                new Tag { tagName = "Rated EC"},
                new Tag { tagName = "Rated E"},
                new Tag { tagName = "Rated E10+"},
                new Tag { tagName = "Rated T"},
                new Tag { tagName = "Rated M"},
                new Tag { tagName = "Rated AO"}
            );
        }
    }
}
