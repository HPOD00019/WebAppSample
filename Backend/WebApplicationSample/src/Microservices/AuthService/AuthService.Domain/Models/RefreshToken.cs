
namespace AuthService.Domain.Models
{
    public class RefreshToken
    {
        public int? Id { get; set; }
        public string Value { get; set; }
        public DateTime ExpireAt { get; set; }
        public int User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; }

        public RefreshToken(int id, string token, DateTime expire, int UserId, bool isActive)
        {
            Id = id;
            Value = token;
            ExpireAt = expire;
            User = UserId;
            IsActive = isActive;
        }
        public RefreshToken(RefreshToken token)
        {
            Id = token.Id;
            Value = token.Value;
            ExpireAt = token.ExpireAt;
            User = token.User;
            IsActive = token.IsActive;
            CreatedAt = token.CreatedAt;
        }
        public RefreshToken() { }
    }
}
