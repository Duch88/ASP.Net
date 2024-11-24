using Microsoft.AspNetCore.Mvc;
using ChampionsLeagueTeamsApp.Models;
using ChampionsLeagueTeamsApp.Data;
using Microsoft.EntityFrameworkCore;
using ChampionsLeagueTeamsApp.BusinessLogic;

namespace ChampionsLeagueTeamsApp.Controllers
{
    public class StadiumsController : Controller
    {
        private readonly IStadiumService _stadiumService;


        public StadiumsController(IStadiumService stadiumService)
        {
            _stadiumService = stadiumService;
        }


        public async Task<IActionResult> Index(string? searchQuery = null, int pageNumber = 1, int pageSize = 5)
        {

            IEnumerable<Stadium> stadiums;


            if (!string.IsNullOrEmpty(searchQuery))
            {
                stadiums = await _stadiumService.SearchStadiumsAsync(searchQuery);
                ViewData["CurrentFilter"] = searchQuery;
            }
            else
            {

                stadiums = await _stadiumService.GetStadiumsWithPaginationAsync(pageNumber, pageSize);
            }


            var totalStadiumsCount = await _stadiumService.GetAllStadiumsAsync();
            var totalPages = (int)Math.Ceiling((double)totalStadiumsCount.Count() / pageSize);


            ViewData["PageNumber"] = pageNumber;
            ViewData["TotalPages"] = totalPages;


            return View(stadiums);
        }
    }
}
