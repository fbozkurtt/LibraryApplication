using System;

namespace LibraryApplication.Application.Common.Models
{
    public class LoginResponse
    {
        public string Username { get; set; }

        public string Token { get; set; }

        public DateTime Expires { get; set; }
    }
}
