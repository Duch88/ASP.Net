using Microsoft.AspNetCore.Mvc;
using ChampionsLeagueTeamsApp.Models;

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
                var titles = await _context.Titles.ToListAsync();
                return View(titles);
            }
    }
}
