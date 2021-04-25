using LibraryApplication.Application.Common.Exceptions;
using LibraryApplication.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Commands.Book
{
    public class DeleteBookCommand : IRequest
    {
        public long ISBN { get; set; }
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
            var book = await _dbContext.Books.SingleOrDefaultAsync(b => b.ISBN.Equals(request.ISBN));

            if (book == null)
                throw new NotFoundException(
                    nameof(Domain.Entities.Book),
                    nameof(Domain.Entities.Book.ISBN),
                    request.ISBN);

            if (_dbContext.BookReservations.Any(r => r.Book.Equals(book)))
                throw new Exception("Please wait until all copies returned before deleting this book record.");

            //_dbContext.BookProducts.RemoveRange((await _dbContext.BookProducts.Where(p => p.Book.Equals(book)).ToListAsync()));
            _dbContext.Books.Remove(book);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
