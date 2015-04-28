using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace MySteamPlay.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public ulong SteamId { get; set; }      // unique identifier for this user's Steam account
        public string PersonaName { get; set; } // after reviewing Steam API documentation, still not sure what this is
        public string Avatar { get; set; }      // ditto

        public List<Game> GamesList { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<GameDescrip> GDescriptions { get; set; }

        public DbSet<GameList> GLists { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<TagList> TagLists { get; set; }
    }
}