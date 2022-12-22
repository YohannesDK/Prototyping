using Microsoft.EntityFrameworkCore;
using Service1.Entities;

namespace Service1.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // use in memory database for testing
            options.UseInMemoryDatabase("TestDb");
        }

        public DbSet<Offer> Offer { get; set; }
    }
}
