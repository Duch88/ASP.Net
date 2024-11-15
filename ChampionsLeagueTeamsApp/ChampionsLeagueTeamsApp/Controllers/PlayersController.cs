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

            public async Task<IActionResult> Index()
            {
                var players = await _context.Players.ToListAsync();
                return View(players);
            }
    }
}
