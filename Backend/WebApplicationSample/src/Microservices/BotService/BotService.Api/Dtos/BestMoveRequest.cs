namespace BotService.Api.Dtos
{
    public class BestMoveRequest
    {
        public string Fen { get; set; } = "";
        public int Elo { get; set; } = 1500;
        public int? MoveTime { get; set; } 
    }
}
