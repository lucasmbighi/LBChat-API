using System;
namespace LBChatAPI.Models
{
    public static class ChatRepository
    {
        public static List<Chat> Chats =>
            UserRepository.Users.Select(u =>
            new Chat
            {
                Id = Guid.NewGuid(),
                UserId = u.Id,
                User = u
            }).ToList();

        public static void AddTestChats(ChatContext context)
        {
            context.Chats.AddRange(Chats);
            context.SaveChanges();
        }
    }
}

