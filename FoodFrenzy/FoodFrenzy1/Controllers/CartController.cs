using Microsoft.AspNetCore.Mvc;
using FoodFrenzy1.Models;
using FoodFrenzy1.Data;
using Microsoft.AspNetCore.Authorization;

namespace FoodFrenzy1.Controllers
{
   
    public class CartController : Controller
    {
        private readonly FoodFrenzy1Context _context;
        private readonly Cart _cart;
        public CartController(FoodFrenzy1Context context ,Cart cart)
        {
            _context = context;
            _cart = cart;
        }
        public IActionResult Index()
        {
            var items = _cart.GetAllCartItems();
            _cart.CartItems = items;
            return View(_cart);
        }

        public IActionResult AddToCart(int id)
        {
            var selectedFood = GetBookById(id);
            if(selectedFood != null) 
                {
                    _cart.AddToCart(selectedFood,1);
                }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var selectedFood = GetBookById(id);
            if(selectedFood!=null)
            {
                _cart.RemoveFromCart(selectedFood);
            }
            return RedirectToAction("Index");
        }

        public IActionResult ReduceQuantity(int id)
        {
            var selectedFood = GetBookById(id);
            if (selectedFood != null)
            {
                _cart.ReduceQuantity(selectedFood);
            }
            return RedirectToAction("Index");
        }
        public IActionResult IncreaseQuantity(int id)
        {
            var selectedFood = GetBookById(id);
            if (selectedFood != null)
            {
                _cart.IncreaseQuantity(selectedFood);
            }
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            _cart.ClearCart();
            return RedirectToAction("Index");
        }
        public Product GetBookById (int id)
        {
            return _context.Product.FirstOrDefault(b => b.id == id);
        }
    }
}
