using LibraryApplication.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApplication.Domain.Entities
{
    // Class that represents each physical copy of a book.
    public class BookCopy : BaseEntity, IHasDomainEvent
    {
        //public Guid BookMetaId { get; set; }

        //public Guid? BookReservationId { get; set; }

        [Required]
        public string QRCode { get; set; }

        public bool Reserved { get; set; }

        public virtual BookMeta BookMeta { get; set; }

        //public virtual BookReservation BookReservation { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
