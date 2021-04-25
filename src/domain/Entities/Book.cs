using LibraryApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryApplication.Domain.Entities
{
    public class Book : BaseEntity, IHasDomainEvent
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }

        public long ISBN { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public virtual List<BookProduct> BooksInInventory { get; set; } = new List<BookProduct>();

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
