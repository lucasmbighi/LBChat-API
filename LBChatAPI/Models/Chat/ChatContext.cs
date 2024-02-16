using Microsoft.EntityFrameworkCore;

namespace LBChatAPI.Models
{
	public class ChatContext : DbContext
	{
        public ChatContext(DbContextOptions<ChatContext> options)
			: base(options)
		{
		}

		public DbSet<Chat> Chats { get; set; } = null!;
    }
}

