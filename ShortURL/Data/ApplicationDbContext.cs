using Microsoft.EntityFrameworkCore;
using ShortURL.Models.DB;

namespace ShortURL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Url> Urls { get; set; }
    }
}
