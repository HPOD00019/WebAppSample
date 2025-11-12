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

        public async Task<User?> GetUserById(Guid TargetId)
        {
            var user = _context.Users.FirstOrDefault(user =>  user.Id == TargetId );
            return user;

        }

        public async Task<Guid> RegisterUser(User user)
        {

            if(user.Id == Guid.Empty)
            {
                var id = Guid.NewGuid();
                user.Id = id;
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return user.Id;
        }
    }
}
