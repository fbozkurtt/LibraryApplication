using LibraryApplication.Domain.Common;
using LibraryApplication.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

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
