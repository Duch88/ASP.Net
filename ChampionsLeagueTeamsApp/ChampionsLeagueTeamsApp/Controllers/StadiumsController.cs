using Microsoft.AspNetCore.Mvc;
using ChampionsLeagueTeamsApp.Models;

namespace ChampionsLeagueTeamsApp.Controllers
{
    public class StadiumsController : Controller
    {
        public class StadiumsController : Controller
        {
            private readonly ApplicationDbContext _context;

            public StadiumsController(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Index()
            {
                var stadiums = await _context.Stadiums.ToListAsync();
                return View(stadiums);
            }
        }
    }
}
