using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.DataBaseContext
{
    class UserAuthDbContext : DbContext
    {
        public UserAuthDbContext(DbContextOptions<UserAuthDbContext> options) : base(options)
        {

        }


    }
}
