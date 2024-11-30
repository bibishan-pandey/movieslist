using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MovieList.Models;

namespace MovieList.Controllers
{
    public class MovieController : Controller
    {
        private MovieContext context { get; set; }

        public MovieController(MovieContext ctx)
        {
            context = ctx;
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";

            // Check if Genres exist, if not show an error
            var genres = context.Genres.OrderBy(g => g.Name).ToList();
            if (!genres.Any())
            {
                ViewBag.NoGenresMessage = "No genres available. You can add a new genre.";
            }
            ViewBag.Genres = genres;
            return View("Edit", new Movie());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var movie = context.Movies.Find(id);

            var genres = context.Genres.OrderBy(g => g.Name).ToList();
            if (!genres.Any())
            {
                ViewBag.NoGenresMessage = "No genres available. You can add a new genre.";
            }
            ViewBag.Genres = genres;
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                if (movie.MovieId == 0)
                    context.Movies.Add(movie);
                else
                    context.Movies.Update(movie);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Action = (movie.MovieId == 0) ? "Add" : "Edit";
                var genres = context.Genres.OrderBy(g => g.Name).ToList();
                if (!genres.Any())
                {
                    ViewBag.NoGenresMessage = "No genres available. You can add a new genre.";
                }
                ViewBag.Genres = genres;
                return View(movie);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movie = context.Movies.Find(id);
            return View(movie);
        }
        [HttpPost]
        public IActionResult Delete(Movie movie)
        {
            context.Movies.Remove(movie);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}