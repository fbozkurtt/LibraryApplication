using LibraryApplication.Domain.Common;
using LibraryApplication.Domain.Entities;

namespace LibraryApplication.Domain.Events
{
    public class BookRemovedEvent : DomainEvent
    {
        public BookRemovedEvent(BookMeta item)
        {
            Item = item;
        }

        public BookMeta Item { get; }
    }
}
