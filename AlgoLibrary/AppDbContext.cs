using Microsoft.EntityFrameworkCore;

namespace AlgoLibrary
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public AppDbContext() : base()
        {

        }
    }
}
