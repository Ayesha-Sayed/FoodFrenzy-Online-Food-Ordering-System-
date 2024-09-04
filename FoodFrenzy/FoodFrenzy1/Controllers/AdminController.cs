using Microsoft.AspNetCore.Mvc;

namespace FoodFrenzy1.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
        public IActionResult Login(string Username, string Password)
        {
            try
            {

                if (Username.Equals("FoodFrenzyAdmin") && Password.Equals("FoodFrenzy@admin"))
                {
                    //HttpContext.Session.SetString("admin", Username);
                    return RedirectToAction("Index","FoodMenu");
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
