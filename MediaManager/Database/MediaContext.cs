using MediaManager.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.Database
{
    public class MediaContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(@"Server=localhost;Port=3306;Database=MediaManager;Uid=user;Password=UserPassword;SslMode=none;");
        }
    }
}
