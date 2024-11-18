using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeagueTeamsApp.Controllers
{
    public class ErrorController : Controller
    {

        [Route("Error/404")]
        public IActionResult NotFoundPage()
        {
            return View("NotFound");
        }

        [Route("Error/500")]
        public IActionResult InternalServerError()
        {
            return View("InternalServerError");
        }

    }
}
