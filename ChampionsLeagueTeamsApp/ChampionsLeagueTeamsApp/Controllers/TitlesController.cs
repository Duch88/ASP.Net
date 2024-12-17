using Microsoft.AspNetCore.Mvc;
using ChampionsLeagueTeamsApp.Models;
using ChampionsLeagueTeamsApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace ChampionsLeagueTeamsApp.Controllers
{
    [Authorize]
    public class TitlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TitlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? searchQuery = null, int pageNumber = 1, int pageSize = 5)
        {
            IQueryable<Title> titlesQuery = _context.Titles.Include(t => t.Team);

            if (!string.IsNullOrEmpty(searchQuery))
            {
                titlesQuery = titlesQuery.Where(t => t.Team.Name.Contains(searchQuery));
                ViewData["CurrentFilter"] = searchQuery;
            }

            var totalTitlesCount = await titlesQuery.CountAsync();

            var titles = await titlesQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalTitlesCount / pageSize);

            ViewData["PageNumber"] = pageNumber;
            ViewData["TotalPages"] = totalPages;

            return View(titles);
        }

        public async Task<IActionResult> Details(int id)
        {
            var title = await _context.Titles
                .Include(t => t.Team)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (title == null)
            {
                return NotFound();
            }

            return View(title);
        }

       

        
    }
}