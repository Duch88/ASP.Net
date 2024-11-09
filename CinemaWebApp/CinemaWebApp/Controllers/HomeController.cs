using CinemaWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CinemaWebApp.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            
            ViewBag.Message = "Welcome to the Cinema Web App";

            return View();
        }

        
    }
}
