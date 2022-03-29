using GreenUp.Core.Business.Addresses.Models;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Orders.Models;
using GreenUp.Core.Business.Participations.Models;
using GreenUp.Core.Business.Products.Models;
using GreenUp.Core.Business.Tags.Models;
using GreenUp.Core.Business.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenUp.EntityFrameworkCore.Data
{
    public class GreenUpContext : DbContext
    {
        public GreenUpContext(DbContextOptions<GreenUpContext> options) : base (options)
        {}

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Adresses { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<MissionTask> Tasks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketProducts> BasketProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participation>().HasKey(m => new { m.UserId, m.MissionId });

            modelBuilder.Entity<Favorite>().HasKey(f => new { f.UserId, f.ProductId });

            modelBuilder.Entity<BasketProducts>().HasKey(bp => new { bp.ProductId, bp.BasketId });
        
            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.Delivery)
            //    .WithMany(d => d.Orders)
            //    .IsRequired(true);
            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.Billing)
            //    .WithMany(d => d.Orders)
            //    .IsRequired(true);
        }
    }
}
