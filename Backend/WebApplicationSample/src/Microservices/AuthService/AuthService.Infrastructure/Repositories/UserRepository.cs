
using AuthService.Domain.Models;
using AuthService.Domain.Repositories;
using AuthService.Infrastructure.DataBaseContext;
using Microsoft.EntityFrameworkCore;


namespace AuthService.Infrastructure.Repositories
{
    class UserRepository : IUserRepository
    {
        private AuthMicroserviceDbContext _context;

        public UserRepository(AuthMicroserviceDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserById(int TargetId)
        {
            var user = _context.Users.FirstOrDefault(user =>  user.Id == TargetId );
            return user;

        }

        public async Task<User?> GetUserByName(string username)
        {
            var user = _context.Users.FirstOrDefault(user => user.UserName == username);
            return user;

        }

        public async Task<bool> IsUserNameExists(string username)
        {
            var _user = await _context.Users.AnyAsync(u => u.UserName == username);
            if (_user) return true;
            else return false;
        }

        public async Task<int> RegisterUser(User _user)
        {
            var user = new User(_user);
            if(user.Id == null)
            {
                var maxId = await _context.Users.AsNoTracking().MaxAsync(u => (int?)u.Id) ?? 0;

                user.Id = maxId + 1;
            }
            if(user.Rating == null || user.Rating == 0)
            {
                user.Rating = 100;
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return user.Id.Value;
        }

    }
}
