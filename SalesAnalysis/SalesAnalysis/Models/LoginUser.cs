using System;
using System.Collections.Generic;

namespace SalesAnalysis.Models
{
    public class LoginUser
    {
        private static readonly Lazy<IReadOnlyList<LoginUser>> _definedUsers = new Lazy<IReadOnlyList<LoginUser>>(InitDefinedUsers);

        private static IReadOnlyList<LoginUser> InitDefinedUsers()
        {
            return new List<LoginUser>
            {
                new LoginUser("test@test.com", "C1_test", "Admin"),
                new LoginUser("nancy@test.com", "C1_test", "Sales Representative"),
                new LoginUser("steven@test.com", "C1_test", "Sales Manager" )
            }.AsReadOnly();
        }

        public static IReadOnlyList<LoginUser> DefinedUsers
        {
            get
            {
                return _definedUsers.Value;
            }
        }

        public LoginUser(string email, string password, string role)
        {
            Email = email;
            Password = password;
            Role = role;
        }

        public string Email { get; }
        public string Password { get; }
        public string Role { get; }
        public string Display { get { return string.Format("{0}({1})", Email, Role); } }
    }
}