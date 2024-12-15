using Microsoft.AspNetCore.Mvc;
using ChampionsLeagueTeamsApp.Models;
using ChampionsLeagueTeamsApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace ChampionsLeagueTeamsApp.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> Details(int id)
        {
            var coach = await _context.Coaches
                .Include(c => c.Team)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }

        
        public IActionResult Create()
        {
            // Ако искате да изберете отбор
            ViewBag.Teams = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Coach coach)
        {
            if (ModelState.IsValid)
            {
                _context.Coaches.Add(coach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            
            ViewBag.Teams = new SelectList(_context.Teams, "Id", "Name");
            return View(coach);
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null)
            {
                return NotFound();
            }

            ViewBag.Teams = new SelectList(_context.Teams, "Id", "Name", coach.TeamId);
            return View(coach);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Coach coach)
        {
            if (id != coach.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoachExists(coach.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Teams = new SelectList(_context.Teams, "Id", "Name", coach.TeamId);
            return View(coach);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var coach = await _context.Coaches
                .Include(c => c.Team)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach != null)
            {
                _context.Coaches.Remove(coach);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CoachExists(int id)
        {
            return _context.Coaches.Any(e => e.Id == id);
        }

    }
}