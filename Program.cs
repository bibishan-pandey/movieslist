using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieList.Encryption;
using MovieList.Models;

namespace MovieList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add the AuthContext to the services container
            builder.Services.AddDbContext<AuthContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("MovieContext");
                options.UseSqlServer(connectionString);
            });
            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;

                    // Encryption and Decryption
                    options.Stores.ProtectPersonalData = true;
                })
                .AddEntityFrameworkStores<AuthContext>()
                .AddDefaultTokenProviders()
                .AddPersonalDataProtection<CustomLookupProtector, CustomLookupProtectorKeyRing>(); ;
            builder.Services.AddRazorPages();

            // Add the MovieContext to the services container
            builder.Services.AddDbContext<MovieContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("MovieContext");
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");

            app.MapRazorPages();
            app.Run();
        }
    }
}
