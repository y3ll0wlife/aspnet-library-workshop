using LibraryProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Dto;
using LibraryProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryProject.Controllers
{
    public class BookController(LibraryContext context) : Controller
    {
        private readonly LibraryContext _context = context;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteBook(int id)
        {
            _context.Books.Where(x => x.Id == id).ExecuteDelete();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult UpdateBook(int id)
        {
            var book = _context.Books.First(x => x.Id == id);
            var authors = _context.Authors
                .Select(x => new SelectListItem($"{x.Firstname} {x.Surname}", x.Id.ToString()))
                .ToList();

            ViewBag.Authors = authors;

            return View(book);
        }

        [HttpPost]
        public IActionResult UpdateBook(UpdateBookDto update)
        {
            if (!ModelState.IsValid)
            {
                return UpdateBook(update.Id);
            }

            var book = _context.Books.First(x => x.Id == update.Id);
            book.Titel = update.Title;
            book.AmountOfPages = update.AmountOfPages;
            book.AuthorId = update.AuthorId;

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            var authors = _context.Authors
                .Select(x => new SelectListItem($"{x.Firstname} {x.Surname}", x.Id.ToString()))
                .ToList();

            ViewBag.Authors = authors;

            var genres = _context.Genres
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            ViewBag.Genres = genres;

            return View();
        }

        [HttpPost]
        public IActionResult AddBook(AddBookDto addBook)
        {
            if (!ModelState.IsValid)
            {
                return AddBook();
            }

            var book = new Book()
            {
                AmountOfPages = addBook.AmountOfPages,
                AuthorId = addBook.AuthorId,
                Published = DateTime.Now,
                Titel = addBook.Title
            };

            _context.Books.Add(book);

            foreach (var id in addBook.GenreIds)
            {
                var genre = _context.Genres.Find(id);
                book.Genres.Add(genre);
            }
            
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AboutBook(int id)
        {
            var book = _context.Books
                .Include(x => x.Author)
                .First(x => x.Id == id);

            return View(book);
        }
    }
}
