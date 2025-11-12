using AuthService.Domain.Models;



namespace AuthService.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<Guid> RegisterUser(User user);
        public Task<User?> GetUserById(Guid id);
    }
}
