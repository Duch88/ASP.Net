using Microsoft.AspNetCore.Mvc;
using ChampionsLeagueTeamsApp.Models;
using ChampionsLeagueTeamsApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ChampionsLeagueTeamsApp.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string? searchQuery = null, int pageNumber = 1, int pageSize = 5)
        {

            IQueryable<Player> playersQuery = _context.Players.Include(p => p.Team);


            if (!string.IsNullOrEmpty(searchQuery))
            {
                playersQuery = playersQuery.Where(p => p.Name.Contains(searchQuery) || p.Team.Name.Contains(searchQuery));
                ViewData["CurrentFilter"] = searchQuery;
            }


            var totalPlayersCount = await playersQuery.CountAsync();


            var players = await playersQuery
                .Skip((pageNumber - 1) * pageSize)  
                .Take(pageSize)                     
                .ToListAsync();


            var totalPages = (int)Math.Ceiling((double)totalPlayersCount / pageSize);


            ViewData["PageNumber"] = pageNumber;
            ViewData["TotalPages"] = totalPages;

            return View(players);
        }

        public async Task<IActionResult> Details(int id)
        {
            var player = await _context.Players
                .Include(p => p.Team) 
                .FirstOrDefaultAsync(p => p.Id == id); 

            if (player == null)
            {
                return NotFound(); 
            }

            return View(player); 
        }
    }
}
