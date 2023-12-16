using LibraryProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LibraryProject.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibraryProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LibraryContext _context;

        public HomeController(ILogger<HomeController> logger, LibraryContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var books = _context.Books
                .Include(x => x.Author)
                .Include(x => x.Genres)
                .ToList();

            var genres = _context.Genres
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            ViewBag.Genres = genres;

            return View(books);
        }

        [HttpPost]
        public IActionResult Search(List<int> genreIds)
        {
            var bookList = _context.Books
                .Include(x => x.Author)
                .Include(x => x.Genres)
                .ToList();

            var genres = _context.Genres
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            ViewBag.Genres = genres;

            if (genreIds.Count == 0)
            {
                return View(bookList);
            }

            var books = new List<Book>();
            foreach (var book in bookList)
            {
                var bookGenres = book.Genres.Select(x => x.Id).ToList();

                if (!genreIds.Intersect(bookGenres).Any())
                {
                    continue;
                }

                books.Add(book);
            }
            
            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
