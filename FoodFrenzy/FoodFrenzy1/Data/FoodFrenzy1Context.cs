using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodFrenzy1.Models;


namespace FoodFrenzy1.Data
{
    public class FoodFrenzy1Context : DbContext
    {
        public FoodFrenzy1Context (DbContextOptions<FoodFrenzy1Context> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            
            
        }


    }
}
