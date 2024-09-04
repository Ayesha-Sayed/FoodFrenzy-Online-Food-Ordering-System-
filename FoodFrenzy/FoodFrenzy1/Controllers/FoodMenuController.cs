using FoodFrenzy1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodFrenzy1.Controllers
{

    public class FoodMenuController : Controller
    {
        public readonly FoodFrenzy1Context _context;
        public FoodMenuController(FoodFrenzy1Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, string minPrice, string maxPrice)
        {
            var products = _context.Product.Select(b => b);
            if(!string.IsNullOrEmpty(searchString) )
            {
                products = products.Where(b=>b.ProductName.Contains(searchString));
            }

            if(!string.IsNullOrEmpty(minPrice) )
            {
                var min = int.Parse(minPrice);
                products = products.Where(b => b.ProductPrice >= min);
            }

            if (!string.IsNullOrEmpty(maxPrice))
            {
                var max = int.Parse(maxPrice);
                products = products.Where(b => b.ProductPrice <= max);
            }
            return View(await products.ToListAsync());
                        
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

    }
}
