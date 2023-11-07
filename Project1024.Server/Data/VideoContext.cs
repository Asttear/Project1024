using Microsoft.EntityFrameworkCore;
using Project1024.Server.Models;

namespace Project1024.Server.Data
{
    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
        public DbSet<VideoCategory> VideoCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<UserFollower> UserFollowers { get; set; }

        public VideoContext(DbContextOptions<VideoContext> options) : base(options)
        {

        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<VideoCategory>()
        //        .HasMany(e => e.Videos)
        //        .WithOne(e => e.Category)
        //        .HasForeignKey(e => e.category_id);
        //}
    }
}
