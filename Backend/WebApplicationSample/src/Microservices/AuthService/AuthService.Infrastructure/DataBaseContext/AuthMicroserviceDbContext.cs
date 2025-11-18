using AuthService.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace AuthService.Infrastructure.DataBaseContext
{
    class AuthMicroserviceDbContext : DbContext
    {
        public AuthMicroserviceDbContext(DbContextOptions<AuthMicroserviceDbContext> options) : base(options)
        {

        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
