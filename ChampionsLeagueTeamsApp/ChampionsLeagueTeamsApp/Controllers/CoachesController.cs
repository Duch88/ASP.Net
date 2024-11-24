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

        public async Task<IActionResult> Index(string? searchQuery = null, int pageNumber = 1, int pageSize = 5)
        {

            IQueryable<Coach> coachesQuery = _context.Coaches.Include(c => c.Team);


            if (!string.IsNullOrEmpty(searchQuery))
            {
                coachesQuery = coachesQuery.Where(c =>
                    c.Name.Contains(searchQuery) ||
                    c.Team.Name.Contains(searchQuery));
                ViewData["CurrentFilter"] = searchQuery; 
            }


            var totalCoachesCount = await coachesQuery.CountAsync();


            var coaches = await coachesQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            var totalPages = (int)Math.Ceiling((double)totalCoachesCount / pageSize);


            ViewData["PageNumber"] = pageNumber;
            ViewData["TotalPages"] = totalPages;

            return View(coaches);
        }
    }
}
