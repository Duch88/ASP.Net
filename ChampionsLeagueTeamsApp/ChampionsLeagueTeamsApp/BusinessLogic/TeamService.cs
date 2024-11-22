using ChampionsLeagueTeamsApp.Data;
using ChampionsLeagueTeamsApp.Models;
using Microsoft.EntityFrameworkCore;


namespace ChampionsLeagueTeamsApp.BusinessLogic
{
    public class TeamService : ITeamService
    {
        
            private readonly ApplicationDbContext _context;

            public TeamService(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Team>> GetAllTeamsAsync()
            {
                return await _context.Teams.ToListAsync();
            }

            public async Task<IEnumerable<Team>> GetTeamsWithPaginationAsync(int pageNumber, int pageSize)
            {
                return await _context.Teams
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            public async Task<IEnumerable<Team>> SearchTeamsAsync(string searchQuery)
            {
                return await _context.Teams
                    .Where(t => t.Name.Contains(searchQuery) || t.Country.Contains(searchQuery))
                    .ToListAsync();
            }

            public async Task<int> GetTotalTeamsCountAsync()
            {
                return await _context.Teams.CountAsync();
            }

    }

}
