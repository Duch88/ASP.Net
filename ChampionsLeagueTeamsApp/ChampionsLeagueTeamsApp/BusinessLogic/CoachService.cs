using ChampionsLeagueTeamsApp.Data;
using ChampionsLeagueTeamsApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ChampionsLeagueTeamsApp.BusinessLogic
{
    public class CoachService : ICoachService
    {
        
        
            private readonly ApplicationDbContext _context;

            public CoachService(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Coach>> GetAllCoachesAsync()
            {
                return await _context.Coaches.Include(c => c.Team).ToListAsync();
            }

            public async Task<IEnumerable<Coach>> SearchCoachesAsync(string searchQuery)
            {
                if (string.IsNullOrEmpty(searchQuery))
                {
                    return await _context.Coaches.Include(c => c.Team).ToListAsync();
                }

                return await _context.Coaches
                    .Include(c => c.Team)
                    .Where(c => c.Name.Contains(searchQuery) || c.Team.Name.Contains(searchQuery))
                    .ToListAsync();
            }

            public async Task<IEnumerable<Coach>> GetCoachesWithPaginationAsync(int pageNumber, int pageSize)
            {
                return await _context.Coaches
                    .Include(c => c.Team)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        
    }
}
