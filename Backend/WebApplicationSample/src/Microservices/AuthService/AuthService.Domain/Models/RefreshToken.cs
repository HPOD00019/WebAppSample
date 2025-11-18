
namespace AuthService.Domain.Models
{
    public class RefreshToken
    {
        public required Guid Id { get; init; }
        public required string Value { get; init; }
        public DateTime ExpireAt { get; set; }
        public Guid User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; }
    }
}
