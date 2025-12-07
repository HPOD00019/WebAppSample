using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Models
{
    public class User
    {
        public int? Rating { get; set; }
        public int? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public UserRole? Role { get; set; }

        public User()
        {

        }
        public User(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
            PasswordHash = user.PasswordHash;
            PasswordSalt = user.PasswordSalt;
            Role = user.Role;
            CreatedAt = user.CreatedAt;
            Role = user.Role;
        }
    }
}
