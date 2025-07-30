using AuthService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.DataBaseContext
{
    class AuthMicroserviceDbContext : DbContext
    {
        public AuthMicroserviceDbContext(DbContextOptions<AuthMicroserviceDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

    }
}
