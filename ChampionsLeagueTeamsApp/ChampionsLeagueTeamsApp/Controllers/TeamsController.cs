using Microsoft.AspNetCore.Mvc;
using ChampionsLeagueTeamsApp.Models;
using ChampionsLeagueTeamsApp.Data;
using Microsoft.EntityFrameworkCore;
using ChampionsLeagueTeamsApp.BusinessLogic;

namespace ChampionsLeagueTeamsApp.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5, string? searchQuery = null)
        {
            
            IEnumerable<Team> teams;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                teams = await _teamService.SearchTeamsAsync(searchQuery);
                ViewData["SearchQuery"] = searchQuery;
            }
            else
            {
                
                teams = await _teamService.GetTeamsWithPaginationAsync(pageNumber, pageSize);

              
                int totalTeams = await _teamService.GetTotalTeamsCountAsync();
                ViewData["PageNumber"] = pageNumber;
                ViewData["PageSize"] = pageSize;
                ViewData["TotalPages"] = (int)Math.Ceiling((double)totalTeams / pageSize);
            }

            return View(teams);
        }
    }
}
