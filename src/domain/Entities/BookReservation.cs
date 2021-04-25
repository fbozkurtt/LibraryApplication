using LibraryApplication.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApplication.Domain.Entities
{
    public class BookReservation : BaseEntity, IHasDomainEvent
    {
        public Guid UserId { get; set; }

        public Guid BookProductId { get; set; }

        public bool Returned { get; set; }

        public decimal DailyExpirationFee { get; set; }

        public decimal TotalFee { get; set; }

        public DateTime ReservationEnds { get; set; }

        public virtual IdentityUser<Guid> User { get; set; }

        public virtual BookProduct Book { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
