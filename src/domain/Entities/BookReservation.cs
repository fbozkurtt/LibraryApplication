using LibraryApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApplication.Domain.Entities
{
    public class BookReservation : BaseEntity, IHasDomainEvent
    {
        public Guid UserId { get; set; }

        public bool Returned { get; set; }

        public decimal DailyExpirationFee { get; set; }

        public decimal? TotalFee { get; set; }

        public DateTime ReservationEnds { get; set; }

        public virtual BookMeta BookMeta { get; set; }

        public virtual BookCopy BookCopy { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
