using LibraryApplication.Domain.Common;
using LibraryApplication.Domain.Entities;

namespace LibraryApplication.Domain.Events
{
    public class BookReturnedEvent : DomainEvent
    {
        public BookReturnedEvent(BookReservation item)
        {
            Item = item;
        }

        public BookReservation Item { get; }
    }
}
