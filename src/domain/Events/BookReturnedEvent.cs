using LibraryApplication.Domain.Common;
using LibraryApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
