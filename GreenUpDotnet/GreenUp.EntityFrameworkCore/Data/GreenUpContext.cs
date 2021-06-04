using GreenUp.Core.Business.Associations.Models;
using GreenUp.Core.Business.Companies.Models;
using GreenUp.Core.Business.Locations.Models;
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
        public DbSet<Location> Locations { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<Mission>()
                .HasOne(m => m.Place)
                .WithOne(m => m.Mission)
                .HasForeignKey<Location>(m => m.MissionId);
            // configures one-to-many relationship
            modelBuilder.Entity<User>()
                .HasOne(m => m.Adress)
                .WithOne(m => m.User)
                .HasForeignKey<Location>(m => m.UserId);
        }
    }
}
