using LibraryApplication.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApplication.Domain.Entities
{
    public class BookMeta : BaseEntity, IHasDomainEvent
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string ISBN { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public virtual List<BookCopy> BooksInInventory { get; set; }

        public virtual List<BookReservation> BookReservations { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
