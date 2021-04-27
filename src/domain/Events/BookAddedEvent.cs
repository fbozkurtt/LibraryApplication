using LibraryApplication.Domain.Common;
using LibraryApplication.Domain.Entities;

namespace LibraryApplication.Domain.Events
{
    public class BookAddedEvent : DomainEvent
    {
        public BookAddedEvent(BookCopy item)
        {
            Item = item;
        }

        public BookCopy Item { get; }
    }
}
