using AuthService.Domain.Models;



namespace AuthService.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<int> RegisterUser(User user);
        public Task<User?> GetUserById(int id);
    }
}
