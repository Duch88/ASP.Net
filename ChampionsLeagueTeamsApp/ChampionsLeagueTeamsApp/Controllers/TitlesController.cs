using Microsoft.AspNetCore.Mvc;
using ChampionsLeagueTeamsApp.Models;
using ChampionsLeagueTeamsApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ChampionsLeagueTeamsApp.Controllers
{
    public class TitlesController : Controller
    {
        
            private readonly ApplicationDbContext _context;

            public TitlesController(ApplicationDbContext context)
            {
                _context = context;
            }

        public async Task<IActionResult> Index()
        {
            var titles = await _context.Titles
                .Include(t => t.Team)
                .ToListAsync();

            return View(titles);

        }
    }
}
