using ChampionsLeagueTeamsApp.Data;
using ChampionsLeagueTeamsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChampionsLeagueTeamsApp.BusinessLogic
{
    public class StadiumService : IStadiumService
    {
        private readonly ApplicationDbContext _context;

        public StadiumService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Stadium>> GetAllStadiumsAsync()
        {
            return await _context.Stadiums.ToListAsync();
        }


        public async Task<IEnumerable<Stadium>> SearchStadiumsAsync(string searchQuery)
        {
            return await _context.Stadiums
                .Where(s => s.Name.Contains(searchQuery) || s.Location.Contains(searchQuery))
                .ToListAsync();
        }


        public async Task<IEnumerable<Stadium>> GetStadiumsWithPaginationAsync(int pageNumber, int pageSize)
        {
            return await _context.Stadiums
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
