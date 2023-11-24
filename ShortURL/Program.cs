using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShortURL.Data;
using ShortURL.Models.DB;
using System.Text.Unicode;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapGet("/{urlString}", (string urlString, ApplicationDbContext db) =>
        {
            var shortenedUrl = db.Urls.FirstOrDefault(x => x.GuidString == urlString);
            if (shortenedUrl != null)
            {

                shortenedUrl.TransitionСount++;
                db.SaveChanges();
                return Results.Redirect(shortenedUrl.LongUrl);
            }
            else return Results.NotFound();
        });

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}