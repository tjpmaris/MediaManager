using MediaManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaManager.Database
{
    public class MediaContext : DbContext
    {
        public MediaContext(DbContextOptions<MediaContext> options)
            : base(options)
        { }

        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
