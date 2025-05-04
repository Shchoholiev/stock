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

        public DbSet<Item> Items { get; set; }
    }       
}
