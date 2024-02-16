namespace LBChatAPI.Models
{
	public static class UserHandler
	{
		private static HashSet<ConnectedUser> _connectedUsers = new();

		public static ConnectedUser? UserById(Guid? id)
		{
			return _connectedUsers
				.Where(c => c.Id == id)
				.FirstOrDefault();
		}

        public static void RemoveUserById(Guid? id)
        {
            var user = UserById(id);

            if (user != null)
            {
                _connectedUsers.Remove(user);
            }
        }

        public static void SetUser(ConnectedUser user)
		{
			_connectedUsers.Add(user);
        }
    }
}

