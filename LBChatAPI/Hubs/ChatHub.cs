using LBChatAPI.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;

namespace LBChatAPI.Models.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly UserContext _context;

        public ChatHub(UserContext context)
        {
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {
            var user = await _context.Users
                .FindAsync(GetContextUserId());

            if (user != null)
            {
                var connectedUser = new ConnectedUser()
                {
                    Id = user.Id,
                    ConnectionId = Context.ConnectionId
                };
                UserHandler.SetUser(connectedUser);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            UserHandler.RemoveUserById(GetContextUserId());
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(Message message)
        {
            await Clients.Group(message.ChatId.ToString()).SendAsync("OnReceiveMessage", message);
        }

        private ConnectedUser? GetContextUser()
        {
            var userIdentifier = Context.UserIdentifier;

            if (userIdentifier != null)
            {
                var userId = new Guid(userIdentifier);

                var user = UserHandler.UserById(userId);

                return user;
            }
            return null;
        }

        private Guid? GetContextUserId()
        {
            var contextUserIdentifier = Context.UserIdentifier;

            if (contextUserIdentifier != null)
            {
                return new Guid(contextUserIdentifier);
            }

            return null;
        }
    }
}