using LibraryProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Controllers
{
    public class AuthorController(LibraryContext context) : Controller
    {
        private readonly LibraryContext _context = context;

        public IActionResult Index()
        {
            var authors = _context.Authors
                .Include(x => x.Books)
                .ToList();

            return View(authors);
        }

        [HttpGet]
        public IActionResult DeleteAuthor(int id)
        {
            var books = _context.Books.Where(x => x.AuthorId == id).ToList();
            foreach (var book in books)
            {
                book.AuthorId = null;
            }
            _context.SaveChanges();

            _context.Authors.Where(x => x.Id == id).ExecuteDelete();
            return RedirectToAction("Index", "Author");
        }

        [HttpGet]
        public IActionResult AboutAuthor(int id)
        {
            var author = _context.Authors
                .Include(x => x.Books)
                .Where(x => x.Id == id)
                .ToList();

            return View(author.First());
        }
    }
}
