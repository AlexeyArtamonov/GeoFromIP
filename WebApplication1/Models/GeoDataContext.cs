using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class GeoDataContext : DbContext
    {
        public DbSet<GeoData> GeoDatas { get; set; }
        public GeoDataContext(DbContextOptions<GeoDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
