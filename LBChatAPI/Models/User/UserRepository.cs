using System;
namespace LBChatAPI.Models
{
    public static class UserRepository
    {
        public static User Lucas => new()
        {
            Id = Guid.NewGuid(),
            Name = "Lucas",
            Email = "lucas@email.com",
            Password = "1"
        };

        public static User Alhou => new()
        {
            Id = Guid.NewGuid(),
            Name = "Alhou",
            Email = "alhou@email.com",
            Password = "1"
        };

        public static User Noun => new()
        {
            Id = Guid.NewGuid(),
            Name = "Noun",
            Email = "noun@email.com",
            Password = "1"
        };

        public static User Ruege => new()
        {
            Id = Guid.NewGuid(),
            Name = "Ruege",
            Email = "ruege@email.com",
            Password = "1"
        };

        public static User Vaipo => new()
        {
            Id = Guid.NewGuid(),
            Name = "Vaipo",
            Email = "vaipo@email.com",
            Password = "1"
        };

        public static List<User> Users =>
            new() { Lucas, Alhou, Noun, Ruege, Vaipo };

        public static void AddTestUsers(UserContext context)
        {
            context.Users.AddRange(Users);
            context.SaveChanges();
        }
    }
}

