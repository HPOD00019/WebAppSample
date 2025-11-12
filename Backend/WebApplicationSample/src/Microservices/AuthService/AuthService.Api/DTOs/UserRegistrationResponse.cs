namespace AuthService.Api.DTOs
{
    public class UserRegistrationResponse
    {
        public required string  UserName { get; init; }
        public required string Email { get; init; }
        public required Guid Id { get; init; }
        public  string? AccessToken { get; init; }
        public string? RefreshToken { get; init; }
    }
}
