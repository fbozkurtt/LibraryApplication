using LibraryApplication.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

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
