using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FoodFrenzy1.Data;
namespace FoodFrenzy1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<FoodFrenzy1Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("FoodFrenzy1Context") ?? throw new InvalidOperationException("Connection string 'FoodFrenzy1Context' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<FoodFrenzy1.Models.Cart>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
          


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=SignUp}/{id?}");

            app.Run();
        }
    }
}
