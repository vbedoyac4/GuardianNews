using GuardianNewsApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuardianNewsApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<SearchParam> SearchParams { get; set; }
        public DbSet<News> News { get; set; }
    }
}
