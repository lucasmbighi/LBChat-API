namespace LBChatAPI.Models
{
    public class UserDTO
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public string? ProfileImagePath { get; set; }
    }
}

