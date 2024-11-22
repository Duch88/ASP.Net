using ChampionsLeagueTeamsApp.Data;
using ChampionsLeagueTeamsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChampionsLeagueTeamsApp.BusinessLogic
{
    public class TitlesService : ITitlesService
    {
        private readonly ApplicationDbContext _context;

        public TitlesService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Title>> GetAllTitlesAsync()
        {
            return await _context.Titles.Include(t => t.Team).ToListAsync();
        }


        public async Task<IEnumerable<Title>> GetTitlesWithPaginationAsync(int pageNumber, int pageSize)
        {
            return await _context.Titles
                .Include(t => t.Team)
                .Skip((pageNumber - 1) * pageSize)  
                .Take(pageSize)                     
                .ToListAsync();
        }


        public async Task<IEnumerable<Title>> SearchTitlesAsync(string searchQuery)
        {
            return await _context.Titles
                .Include(t => t.Team)
                .Where(t => t.Team.Name.Contains(searchQuery))  
                .ToListAsync();
        }
    }
}
