using ChampionsLeagueTeamsApp.Data;
using ChampionsLeagueTeamsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChampionsLeagueTeamsApp.BusinessLogic
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext _context;

        public PlayerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetAllPlayersAsync()
        {
            return await _context.Players.Include(p => p.Team).ToListAsync();
        }

        public async Task<IEnumerable<Player>> SearchPlayersAsync(string searchQuery)
        {
            return await _context.Players
                .Include(p => p.Team)
                .Where(p => p.Name.Contains(searchQuery) || p.Team.Name.Contains(searchQuery))
                .ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetPlayersWithPaginationAsync(int pageNumber, int pageSize)
        {
            return await _context.Players
                .Include(p => p.Team)
                .Skip((pageNumber - 1) * pageSize)  
                .Take(pageSize)                    
                .ToListAsync();
        }
    }
}
