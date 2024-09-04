using FoodFrenzy1.Data;
using FoodFrenzy1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodFrenzy1.Controllers
{
    public class UserController : Controller
    {
        private readonly FoodFrenzy1Context _context;

        public UserController(FoodFrenzy1Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("Id,Username,Password,ConfirmPassword,Email,City,Address")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                int result = await _context.SaveChangesAsync();
                ViewBag.message = "User Registeres";
                return RedirectToAction("SignUp");

            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Username, string Password)
        {
            try
            {
                var admin = await _context.Users.FirstOrDefaultAsync(a => a.Username == Username && a.Password == Password);
                if (admin != null)
                {
                   // HttpContext.Session.SetString("username", Username);
                    return RedirectToAction("Index", "FoodMenu");
                }
                else
                {
                    string error = "Invalid Credentials";
                    ViewBag.error = error;
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
