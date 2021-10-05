using GreenUp.Core.Business.Adresses.Models;
using GreenUp.Core.Business.Associations.Models;
using GreenUp.Core.Business.Companies.Models;
using GreenUp.Core.Business.Inscriptions.Models;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenUp.EntityFrameworkCore.Data
{
    public class GreenUpContext : DbContext
    {
        public GreenUpContext(DbContextOptions<GreenUpContext> options) : base (options)
        {}

        public DbSet<User> Users { get; set; }
        public DbSet<Association> Associations { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<MissionUser> MissionUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MissionUser>().HasKey(m => new { m.UserId, m.MissionId });
        }
    }
}
