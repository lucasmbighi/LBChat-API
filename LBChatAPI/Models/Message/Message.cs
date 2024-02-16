using System;
namespace LBChatAPI.Models
{
	public class Message
	{
        public required Guid Id { get; set; }
        public required Guid SenderId { get; set; }
        public required User Sender { get; set; }
        public required Guid ChatId { get; set; }
        public required Chat Chat { get; set; }
    }
}

