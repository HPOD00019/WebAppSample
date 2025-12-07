namespace AuthService.Api.DTOs
{
    public struct UserDTO
    {
        public int? Rating { get; set; }
        public string UserName { get; init; }
        public string Password { get; init; }
        public string? Email { get; init; }
    }
}
