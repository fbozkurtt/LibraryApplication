using LibraryApplication.Domain.Common;
using LibraryApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApplication.Domain.Events
{
    public class BookAddedEvent : DomainEvent
    {
        public BookAddedEvent(BookProduct item)
        {
            Item = item;
        }

        public BookProduct Item { get; }
    }
}
