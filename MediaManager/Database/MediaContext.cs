using MediaManager.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.Database
{
    public class MediaContext : DbContext
    {
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(@"Server=localhost;Port=3306;Database=MediaManager;Uid=user;Password=UserPassword;SslMode=none;");
        }
    }
}
