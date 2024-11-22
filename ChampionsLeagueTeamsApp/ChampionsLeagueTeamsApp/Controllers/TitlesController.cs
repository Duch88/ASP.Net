using Microsoft.AspNetCore.Mvc;
using ChampionsLeagueTeamsApp.Models;
using ChampionsLeagueTeamsApp.Data;
using Microsoft.EntityFrameworkCore;
using ChampionsLeagueTeamsApp.BusinessLogic;

namespace ChampionsLeagueTeamsApp.Controllers
{
    public class TitlesController : Controller
    {
        private readonly ITitlesService _titlesService;

        public TitlesController(ITitlesService titlesService)
        {
            _titlesService = titlesService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5, string? searchQuery = null)
        {

            var titlesQuery = await _titlesService.GetAllTitlesAsync();


            if (!string.IsNullOrEmpty(searchQuery))
            {
                titlesQuery = titlesQuery.Where(t => t.Team.Name.Contains(searchQuery));
                ViewData["CurrentFilter"] = searchQuery;
            }


            var totalTitlesCount = titlesQuery.Count();
            var titles = titlesQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();


            var totalPages = (int)Math.Ceiling((double)totalTitlesCount / pageSize);


            ViewData["PageNumber"] = pageNumber;
            ViewData["TotalPages"] = totalPages;

            return View(titles);  
        }

    }
}
