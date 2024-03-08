using Microsoft.EntityFrameworkCore;
using AlgoLibrary.Models;

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

        public DbSet<UserModel> User { get; set; }
        public DbSet<ThemeModel> Theme { get; set; }
        public DbSet<ArticleModel> Article { get; set; }
        public DbSet<FolderModel> Folder { get; set; }

    }
}
