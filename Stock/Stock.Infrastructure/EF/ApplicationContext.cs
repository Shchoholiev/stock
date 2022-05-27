using Microsoft.EntityFrameworkCore;
using Stock.Core.Entities;

namespace Stock.Infrastructure.EF
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"server=(LocalDb)\MSSQLLocalDB;database=Stock;integrated security=True;
                    MultipleActiveResultSets=True;App=EntityFramework;";
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Item> Items { get; set; }
    }       
}
