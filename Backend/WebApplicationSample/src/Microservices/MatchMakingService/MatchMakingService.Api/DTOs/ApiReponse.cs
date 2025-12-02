namespace MatchMakingService.Api.DTOs
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
