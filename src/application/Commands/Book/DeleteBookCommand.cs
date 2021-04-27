using LibraryApplication.Application.Common.Exceptions;
using LibraryApplication.Application.Common.Interfaces;
using LibraryApplication.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Commands.Book
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class DeleteBookCommand : IRequest
    {
        public long ISBN { get; }
    }

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteBookCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _dbContext.BookMetas.SingleOrDefaultAsync(b => b.ISBN.Equals(request.ISBN));

            if (book == null)
                throw new NotFoundException(
                    nameof(Domain.Entities.BookMeta),
                    nameof(Domain.Entities.BookMeta.ISBN),
                    request.ISBN);

            if (_dbContext.BookReservations.Any(r => r.BookCopy.Equals(book)))
                throw new Exception("Please wait until all copies returned before deleting this book record.");

            //_dbContext.BookProducts.RemoveRange((await _dbContext.BookProducts.Where(p => p.Book.Equals(book)).ToListAsync()));
            _dbContext.BookMetas.Remove(book);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
