using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieList.Models;

namespace MovieList.Controllers
{
    public class HomeController : Controller
    {
        private MovieContext context { get; set; }
        //private readonly ILogger<HomeController> _logger;
        public HomeController(MovieContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var movies = context.Movies.Include(m => m.Genre).OrderBy(m => m.Name).ToList();

            if (!movies.Any())
            {
                // Handle empty or null movie list
                TempData["Message"] = "No movies available.";
            }
            return View(movies);
        }
    }
}