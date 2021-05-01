using UserModel.Models;
using FossilModel.Models;
using DigSiteModel.Models;
using MuseumModel.Models;
using Microsoft.EntityFrameworkCore;

namespace FossilDigContext.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users {get; set;}
        public DbSet<Fossil> Fossils {get; set;}
        public DbSet<DigSite> DigSites {get; set;}
        public DbSet<Museum> Museums {get; set;}
    }
}