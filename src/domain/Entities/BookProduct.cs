using LibraryApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryApplication.Domain.Entities
{
    // Class that represents each physical copy of a book.
    public class BookProduct : BaseEntity, IHasDomainEvent
    {
        public Guid BookId { get; set; }

        public Guid? BookReservationId { get; set; }

        [Required]
        public string QRCode { get; set; }

        public bool Reserved { get; set; }

        public virtual Book Book { get; set; }

        public virtual BookReservation BookReservation { get; set; }

        public List<DomainEvent> DomainEvents { get; set; }
    }
}
