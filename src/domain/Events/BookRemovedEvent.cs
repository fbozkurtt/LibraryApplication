using LibraryApplication.Domain.Common;
using LibraryApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApplication.Domain.Events
{
    public class BookRemovedEvent : DomainEvent
    {
        public BookRemovedEvent(Book item)
        {
            Item = item;
        }

        public Book Item { get; }
    }
}
