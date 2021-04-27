using LibraryApplication.Domain.Common;
using LibraryApplication.Domain.Entities;

namespace LibraryApplication.Domain.Events
{
    public class BookCreatedEvent : DomainEvent
    {
        public BookCreatedEvent(BookMeta item)
        {
            Item = item;
        }

        public BookMeta Item { get; }
    }
}
