using MatchMakingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace MatchMakingService.Infrastructure.DBcontext
{
    public class MatchMakingMicroserviceDbContext : DbContext
    {
        public MatchMakingMicroserviceDbContext(DbContextOptions<MatchMakingMicroserviceDbContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
    }
}
