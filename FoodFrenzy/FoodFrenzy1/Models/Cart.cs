/*using FoodFrenzy1.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodFrenzy1.Models
{
    public class Cart
    {
        private readonly FoodFrenzy1Context _context;
        public Cart(FoodFrenzy1Context context)
        {
            _context = context;
        }
        public string Id { get; set; }
        public List<CartItem> CartItems { get; set; }
        
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<HttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<FoodFrenzy1Context>();
            string cartId = session.GetString(key: "id") ?? Guid.NewGuid().ToString();
            session.SetString(key: "id", cartId);
            return new Cart(context) { Id = cartId };
        }
        public CartItem GetCartItem(Product product)
        {
            return _context.CartItems.SingleOrDefault(ci =>
            ci.Product.id == product.id && ci.CartId == Id);
        }

        public void AddToCart(Product product, int quantity)
        {
            var cartItem = GetCartItem(product);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Product = product,
                    Quantity = quantity,
                    CartId = "Added"
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }
            _context.SaveChanges();
        }

        public int ReduceQuantity(Product product)
        {
            var cartItem = GetCartItem(product);
            var remainingQuantity = 0;
            if(cartItem != null)
            {
                if(cartItem.Quantity > 1)
                {
                    remainingQuantity = --cartItem.Quantity;
                }
                else
                {
                    _context.CartItems.Remove(cartItem);
                }
            }
            _context.SaveChanges();
            return remainingQuantity;
        }

        public int IncreaseQuantity(Product product)
        {
            var cartItem = GetCartItem(product);
            var remainingQuantity = 0;
            if (cartItem != null)
            {
                if (cartItem.Quantity > 0)
                {
                    remainingQuantity = ++cartItem.Quantity;
                }
            }
            _context.SaveChanges();
            return remainingQuantity;
        }

        public void RemoveFromCart(Product product)
        {
            var cartItem = GetCartItem(product);
            if(cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
            }
            _context.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItem = _context.CartItems.Where(ci => ci.CartId == Id);
            _context.CartItems.RemoveRange(cartItem);
            _context.SaveChanges();
        }
        public List<CartItem> GetAllCartItems()
        {
            return CartItems ??
                (CartItems = _context.CartItems.Where(ci => ci.CartId == Id)
                    .Include(ci => ci.Product)
                    .ToList());
        }

        public decimal GetCartTotal()
        {
            return _context.CartItems
                .Where(ci => ci.CartId == Id)
                .Select(ci => ci.Product.ProductPrice * ci.Quantity)
                .Sum(item=>(decimal)item);
        }

    
    }
}
*/


using FoodFrenzy1.Data;
using FoodFrenzy1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodFrenzy1.Models
{
    public class Cart
    {
        private readonly FoodFrenzy1Context _context;

        public Cart(FoodFrenzy1Context context)
        {
            _context = context;
        }

        public string Id { get; set; }
        public List<CartItem> CartItems { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<FoodFrenzy1Context>();
            string cartId = session.GetString("Id") ?? Guid.NewGuid().ToString();

            session.SetString("Id", cartId);

            return new Cart(context) { Id = cartId };
        }

        public CartItem GetCartItem(Product product)
        {
            return _context.CartItems.SingleOrDefault(ci =>
                ci.Product.id == product.id && ci.CartId == Id);
        }

        public void AddToCart(Product product, int quantity)
        {
            var cartItem = GetCartItem(product);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Product = product,
                    Quantity = quantity,
                    CartId = Id
                };

                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }
            _context.SaveChanges();
        }

        public int ReduceQuantity(Product product)
        {
            var cartItem = GetCartItem(product);
            var remainingQuantity = 0;

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    remainingQuantity = --cartItem.Quantity;
                }
                else
                {
                    _context.CartItems.Remove(cartItem);
                }
            }
            _context.SaveChanges();

            return remainingQuantity;
        }

        public int IncreaseQuantity(Product product)
        {
            var cartItem = GetCartItem(product);
            var remainingQuantity = 0;

            if (cartItem != null)
            {
                if (cartItem.Quantity > 0)
                {
                    remainingQuantity = ++cartItem.Quantity;
                }
            }
            _context.SaveChanges();

            return remainingQuantity;
        }

        public void RemoveFromCart(Product product)
        {
            var cartItem = GetCartItem(product);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
            }
            _context.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = _context.CartItems.Where(ci => ci.CartId == Id);

            _context.CartItems.RemoveRange(cartItems);

            _context.SaveChanges();
        }

        public List<CartItem> GetAllCartItems()
        {
            return CartItems ??
                (CartItems = _context.CartItems.Where(ci => ci.CartId == Id)
                    .Include(ci => ci.Product)
                    .ToList());
        }

        public int GetCartTotal()
        {
            return _context.CartItems
                .Where(ci => ci.CartId == Id)
                .Select(ci => ci.Product.ProductPrice * ci.Quantity)
                .Sum();
        }
    }
}