using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchMakingService.Domain.Entities;
using MatchMakingService.Domain.Repositories;
using MatchMakingService.Infrastructure.DBcontext;
using Microsoft.EntityFrameworkCore;

namespace MatchMakingService.Infrastructure.Repositories
{
    public class PostgresqlUserRepository : IUserRepository
    {
        private readonly MatchMakingMicroserviceDbContext _context;

        public PostgresqlUserRepository(MatchMakingMicroserviceDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserById(int id)
        {
            var user = _context.users.AsNoTracking().First(u => u.Id == id);
            return user;
        }

        public async Task<bool> IsUserExists(User user)
        {
            var _user = _context.users.Any(u => u.Id == user.Id);
            return _user;
        }

        public void RegisterUser(User user)
        {
            _context.users.Add(user);
            _context.SaveChanges();
        }
    }
}
