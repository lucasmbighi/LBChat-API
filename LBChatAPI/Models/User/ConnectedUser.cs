namespace LBChatAPI.Models
{
    public class ConnectedUser
    {
        public required Guid Id { get; set; }
        public required string ConnectionId { get; set; }
    }
}

