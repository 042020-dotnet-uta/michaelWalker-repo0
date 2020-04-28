using System;
using Microsoft.EntityFrameworkCore;

namespace rps.Models
{
    class RPS_DbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnConfig(DbContextOptionsBuilder opts)
        {
            opts.UseSqlite("Data Source=rps.db");
        }
    }
}