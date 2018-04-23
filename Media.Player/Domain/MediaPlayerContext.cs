using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Media.Player.Domain
{
    public class MediaPlayerContext : DbContext
    {
        public DbSet<MediaMetadata> MediaMetadata { get; set; }
        public MediaPlayerContext(DbContextOptions<MediaPlayerContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=mediaplayer.db");
        }
    }
}
