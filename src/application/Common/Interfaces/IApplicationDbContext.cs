using LibraryApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<BookMeta> BookMetas { get; set; }

        public DbSet<BookCopy> BookCopies { get; set; }

        public DbSet<BookReservation> BookReservations { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
