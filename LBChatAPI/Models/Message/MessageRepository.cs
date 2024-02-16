using System;
namespace LBChatAPI.Models
{
    public static class MessageRepository
    {
        public static List<Message> Messages() =>
            ChatRepository
            .Chats
            .Select(c => new Message()
            {
                Id = Guid.NewGuid(),
                SenderId = UserRepository.Lucas.Id,
                Sender = UserRepository.Lucas,
                ChatId = c.Id,
                Chat = c
            })
            .ToList();

        public static void AddTestMessages(MessageContext context)
        {
            context.Messages.AddRange(Messages());
            context.SaveChanges();
        }
    }
}