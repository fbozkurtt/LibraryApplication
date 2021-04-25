using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApplication.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public string UserId { get; }

        public string Username { get; }
    }
}
