using Microsoft.AspNetCore.Mvc;
using CinemaWebApp.Models;
using System.Collections.Generic;
using CinemaWebApp.Models.Data;
using CinemaWebApp.Models.ViewModels;
using System.Reflection;


namespace CinemaWebApp.Controllers
{
    
    public class MovieController : Controller
    {
        private readonly AppDbContext _context;

        public MovieController (AppDbContext context)
        {
            _context = context;
        }

        private static List<Movie> movies = new List<Movie>();
        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }


        [HttpGet]

        public IActionResult Create()
        {
            return View(new MovieViewModel());
        }

        [HttpPost]

        public IActionResult Create(MovieViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie
                {
                    Title = viewModel.Title,
                    Genre = viewModel.Genre,
                    ReleaseDate = viewModel.ReleaseDate,
                    Director = viewModel.Director,
                    Duration = viewModel.Duration,
                    Description = viewModel.Description,
                };


                _context.Movies.Add(movie);
                _context.SaveChanges();

                return RedirectToAction("Index");

            }


            return View(viewModel);

        }

        public IActionResult Details(int id)
        {
            var movie = _context.Movies.Find(id);

            if (movie == null)
            { 
                return NotFound();
            }

            return View(movie);
        }

        [HttpGet]

        public IActionResult AddToProgram(int movieId)
        {
            var movie = _context.Movies.Find(movieId);

            if (movie == null)
            {
                return RedirectToAction("Index");
            }

            var cinemas = _context.Cinemas.ToList();

            var viewModel = new AddMovieToCinemaProgramViewModel
            {
                MovieId = movie.Id,
                MovieTitle = movie.Title,
                Cinemas = cinemas.Select(c => new CinemaCheckBoxItem
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsSelected = false
                }).ToList()

            };

            return View(viewModel);
        }

        [HttpPost]

        public IActionResult AddToProgram(AddMovieToCinemaProgramViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingAssignments = _context.CinemasMovies
                .Where(cm=>cm.MovieId == model.MovieId)
                .ToList();
            _context.RemoveRange(existingAssignments);

            foreach(var cinema in model.Cinemas)
            {
                if (cinema.IsSelected)
                {
                    var cinemaMovie = new CinemaMovie
                    {
                        CinemaId = cinema.Id,
                        MovieId= model.MovieId,
                    };
                    _context.CinemasMovies.Add(cinemaMovie);

                }
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
