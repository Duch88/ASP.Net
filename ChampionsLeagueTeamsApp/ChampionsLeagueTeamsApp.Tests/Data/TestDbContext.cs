using ChampionsLeagueTeamsApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTeamsApp.Tests.Data
{
    public static class TestDbContext
    {
        public static ApplicationDbContext GetInMemoryDbContext()
        {
            
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            
            return new ApplicationDbContext(options);
        }
    }
}
