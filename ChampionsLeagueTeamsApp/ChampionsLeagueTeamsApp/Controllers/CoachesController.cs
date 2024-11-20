using Microsoft.AspNetCore.Mvc;
using ChampionsLeagueTeamsApp.Models;
using ChampionsLeagueTeamsApp.Data;
using Microsoft.EntityFrameworkCore;

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
                var coaches = await _context.Coaches
                            .Include(c => c.Team) 
                            .ToListAsync();

                return View(coaches);
            }
    }
}
