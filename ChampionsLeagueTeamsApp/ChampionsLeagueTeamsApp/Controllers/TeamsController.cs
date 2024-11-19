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

        public async Task<IActionResult> Index()
        {
            var teams = await _teamService.GetAllTeamsAsync(); 
            return View(teams);
        }

        public async Task<IActionResult> Details(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id); 
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }
    }
}
