using LibraryProject.Dto;
using LibraryProject.Model;
using LibraryProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Controllers
{
    [Authorize]
    public class UserController(LibraryContext context, UserManager<User> userManager) : Controller
    {
        private readonly LibraryContext _context = context;
        private readonly UserManager<User> _userManager = userManager;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var dbUser = _context.Users
                .Include(x => x.Image)
                .First(x => x.Id == user.Id);

            string base64 = Convert.ToBase64String(dbUser.Image.Blob);
            ViewBag.Image = $"data:image/png;base64,{base64}";

            return View(dbUser);
        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var dbUser = _context.Users
                .Include(x => x.Image)
                .First(x => x.Id == user.Id);

            return View(dbUser);
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserDto update)
        {
            var data = ReadFully(update.ProfileImage.OpenReadStream());
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var dbUser = _context.Users.Find(user.Id);

            dbUser.Image = new Image()
            {
                Blob = data,
                Published = DateTime.Now
            };

            _context.SaveChanges();

            return RedirectToAction("Index", "User");
        }
    }
}
