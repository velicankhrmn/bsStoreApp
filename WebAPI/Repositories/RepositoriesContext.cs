using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Models.Config;

namespace WebAPI.Repositories
{
    public class RepositoriesContext : DbContext
    {
        public RepositoriesContext(DbContextOptions options) : base(options)
        {
            Console.WriteLine("Çağdaş Kirvemin daşşağını yerim....");
        }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
        }


    }
}
