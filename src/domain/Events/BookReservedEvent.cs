using LibraryApplication.Domain.Common;
using LibraryApplication.Domain.Entities;

namespace LibraryApplication.Domain.Events
{
    public class BookReservedEvent : DomainEvent
    {
        public BookReservedEvent(BookReservation item)
        {
            Item = item;
        }

        public BookReservation Item { get; }
    }
}
