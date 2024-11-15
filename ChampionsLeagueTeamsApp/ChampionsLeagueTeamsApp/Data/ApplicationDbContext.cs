using ChampionsLeagueTeamsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace ChampionsLeagueTeamsApp.Data

{
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

        
            public DbSet<Coach> Coaches { get; set; }
            public DbSet<Player> Players { get; set; }
            public DbSet<Team> Teams { get; set; }
            public DbSet<Title> Titles { get; set; }
            public DbSet<Stadium> Stadiums { get; set; }
        }
    
}
