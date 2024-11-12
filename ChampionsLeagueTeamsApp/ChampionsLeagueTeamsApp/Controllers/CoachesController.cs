using Microsoft.AspNetCore.Mvc;
using ChampionsLeagueTeamsApp.Models;

namespace ChampionsLeagueTeamsApp.Controllers
{
    public class CoachesController : Controller
    {
        
            private readonly ApplicationDbContext _context;

            public CoachesController(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Index()
            {
                var coaches = await _context.Coaches.ToListAsync();
                return View(coaches);
            }
    }
}
