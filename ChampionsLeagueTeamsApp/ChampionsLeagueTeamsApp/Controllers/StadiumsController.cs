using Microsoft.AspNetCore.Mvc;
using ChampionsLeagueTeamsApp.Models;
using ChampionsLeagueTeamsApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ChampionsLeagueTeamsApp.Controllers
{
    public class StadiumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StadiumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index(string? searchQuery = null, int pageNumber = 1, int pageSize = 5)
        {
            
            IQueryable<Stadium> stadiumsQuery = _context.Stadiums.Include(s => s.Team);

            
            if (!string.IsNullOrEmpty(searchQuery))
            {
                stadiumsQuery = stadiumsQuery.Where(s => s.Name.Contains(searchQuery) || s.Location.Contains(searchQuery));
                ViewData["CurrentFilter"] = searchQuery;
            }

            
            var totalStadiumsCount = await stadiumsQuery.CountAsync();

            
            var stadiums = await stadiumsQuery
                .Skip((pageNumber - 1) * pageSize)  
                .Take(pageSize)                     
                .ToListAsync();

            
            var totalPages = (int)Math.Ceiling((double)totalStadiumsCount / pageSize);

            
            ViewData["PageNumber"] = pageNumber;
            ViewData["TotalPages"] = totalPages;

            return View(stadiums);
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var stadium = await _context.Stadiums
                .Include(s => s.Team)  
                .FirstOrDefaultAsync(s => s.Id == id); 

            if (stadium == null)
            {
                return NotFound();  
            }

            return View(stadium);  
        }
    }
}
