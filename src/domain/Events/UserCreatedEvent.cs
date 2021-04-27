using LibraryApplication.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace LibraryApplication.Domain.Events
{
    public class UserCreatedEvent : DomainEvent
    {
        public UserCreatedEvent(IdentityUser item)
        {
            Item = item;
        }

        public IdentityUser Item { get; }
    }
}
