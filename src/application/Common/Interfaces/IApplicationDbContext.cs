using LibraryApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<BookProduct> BookProducts { get; set; }

        public DbSet<BookReservation> BookReservations { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
