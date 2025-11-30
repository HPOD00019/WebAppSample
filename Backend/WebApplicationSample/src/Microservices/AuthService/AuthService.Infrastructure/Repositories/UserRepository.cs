using AuthService.Domain.Models;
using AuthService.Domain.Repositories;
using AuthService.Infrastructure.DataBaseContext;


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

        public async Task<int> RegisterUser(User _user)
        {
            var user = new User(_user);
            if(user.Id == null)
            {
                var u = _context.Users.MaxBy(u => u.Id);
                if (u == null)
                {
                    user.Id = 1;
                }
                user.Id = u.Id + 1;
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return user.Id.Value;
        }

    }
}
