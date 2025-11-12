namespace AuthService.Api.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
