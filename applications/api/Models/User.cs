using System;

namespace API.Models
{
    public class User
    {
        public string Token { get; init; }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}