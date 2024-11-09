using CinemaWebApp.Models.Data;
using CinemaWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using CinemaWebApp.Models;
using Microsoft.EntityFrameworkCore;


namespace CinemaWebApp.Controllers
{
    public class CinemaController : Controller
    {
        private readonly AppDbContext _context;

        public CinemaController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cinemas = _context.Cinemas.ToList();

            var CinemaIndexViewModels = cinemas.Select(c => new CinemaIndexViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location,
            });

            return View(CinemaIndexViewModels);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(CinemaIndexViewModel cinemaIndexViewModel)
        {
            if (ModelState.IsValid)
            {
                var cinema = new Cinema
                {
                    Name = cinemaIndexViewModel.Name,
                    Location = cinemaIndexViewModel.Location
                };

                _context.Cinemas.Add(cinema);
                _context.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(cinemaIndexViewModel);
        }

        
        
            public IActionResult Details(int id)
            {
                var cinema = _context.Cinemas
                    .Include(c => c.CinemaMovies)
                    .ThenInclude(cm => cm.Movie)
                    .FirstOrDefault(c => c.Id == id);

                if (cinema == null)
                {
                    return RedirectToAction("Index");
                }

                var cinemaDetailsViewModel = new CinemaDetailsViewModel
                {
                    Id = cinema.Id,
                    Name = cinema.Name,
                    Location = cinema.Location,
                    Movies = cinema.CinemaMovies.Select(cm => new MovieProgramViewModel
                    {
                        Title = cm.Movie.Title,
                        Duration = cm.Movie.Duration
                    }).ToList()

                };

                return View(cinemaDetailsViewModel);
            }
        
    }
}
