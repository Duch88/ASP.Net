using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeagueTeamsApp.Areas.Admin.Controllers
{
        [Area("Admin")]
        public class DashboardController : Controller
        {
            public IActionResult Index()
            {
                return View();
            }
        }
}
