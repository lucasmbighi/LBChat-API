namespace LBChatAPI.Models
{
	public class User
	{
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? ProfileImagePath { get; set; }
        public ICollection<Chat> Chats { get; } = new List<Chat>();
    }
}