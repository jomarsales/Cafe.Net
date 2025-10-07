using CafeDotNet.Core.Base.Entities;
using CafeDotNet.Core.Users.ValueObjects;

namespace CafeDotNet.Core.Users.Entities
{
    public class User : EntityBase
    {
        public string Username { get; private set; }
        public Password Password { get; private set; }

        protected User() { }

        public User(string username, Password password)
        {
            Username = !string.IsNullOrWhiteSpace(username) ? username : throw new ArgumentException("Username não pode ser vazio.", nameof(username));
            Password = password;
           
            Activate();
        }
    }
}
