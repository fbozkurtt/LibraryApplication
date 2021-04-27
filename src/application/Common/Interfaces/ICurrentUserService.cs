using System;

namespace LibraryApplication.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public Guid UserId { get; }

        public string Username { get; }
    }
}
