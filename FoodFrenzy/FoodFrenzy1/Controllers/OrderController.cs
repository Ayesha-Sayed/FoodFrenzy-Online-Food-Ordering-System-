using FoodFrenzy1.Data;
using FoodFrenzy1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodFrenzy1.Controllers
{
   
    public class OrderController : Controller
    {
        private readonly FoodFrenzy1Context _context;
        private readonly Cart _cart;

        public OrderController(FoodFrenzy1Context context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var cartItems = _cart.GetAllCartItems();
            _cart.CartItems = cartItems;

            if (_cart.CartItems.Count == 0)
            {
                ModelState.AddModelError("", "Cart is empty, please add a book first.");
            }

            if (ModelState.IsValid)
            {
                CreateOrder(order);
                _cart.ClearCart();
                return View("CheckoutComplete", order);
            }

            return View(order);
        }

        public IActionResult CheckoutComplete(Order order)
        {
            return View(order);
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            var cartItems = _cart.CartItems;

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem()
                {
                    Quantity = item.Quantity,
                    ProductId = item.Product.id,
                    OrderId = order.Id,
                    ProductPrice = item.Product.ProductPrice * item.Quantity
                };
                order.OrderItems.Add(orderItem);
                order.OrderTotal += orderItem.ProductPrice;
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

    }
}
