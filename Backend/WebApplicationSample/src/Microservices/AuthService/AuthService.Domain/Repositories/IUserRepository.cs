using AuthService.Domain.Models;



namespace AuthService.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> IsUserNameExists(string userName);
        public Task<User?> GetUserByName(string username);
        public Task<int> RegisterUser(User user);
        public Task<User?> GetUserById(int id);
    }
}
