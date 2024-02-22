using blog_BackEnd.Entites;
using Microsoft.EntityFrameworkCore;

namespace blog_BackEnd.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Post> posts {get; set;}
        public DbSet<Comments> comments {get; set;}
    }
}