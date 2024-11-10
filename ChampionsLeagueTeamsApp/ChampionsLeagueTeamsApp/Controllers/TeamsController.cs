using Microsoft.AspNetCore.Mvc;
using ChampionsLeagueTeamsApp.Models;

namespace ChampionsLeagueTeamsApp.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var teams = await _context.Teams.ToListAsync();
            return View(teams);
        }
    }
}
