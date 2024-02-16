namespace LBChatAPI.Models
{
	public class Chat
	{
		public required Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required User User { get; set; }
		public ICollection<Message> Messages { get; } = new List<Message>();
	}
}

