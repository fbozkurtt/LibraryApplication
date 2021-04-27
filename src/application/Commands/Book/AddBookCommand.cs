using LibraryApplication.Application.Common.Exceptions;
using LibraryApplication.Application.Common.Interfaces;
using LibraryApplication.Constants;
using LibraryApplication.Domain.Entities;
using LibraryApplication.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Commands.Book
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class AddBookCommand : IRequest
    {
        public long ISBN { get; set; }

        [Required]
        public string QRCode { get; set; }
    }

    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddBookCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _dbContext.BookMetas.SingleOrDefaultAsync(b => b.ISBN.Equals(request.ISBN));

            if (book == null)
                throw new NotFoundException(
                    nameof(Domain.Entities.BookMeta),
                    nameof(Domain.Entities.BookMeta.ISBN),
                    request.ISBN);

            var bookProduct = new BookCopy() { QRCode = request.QRCode };

            book.BooksInInventory.Add(new BookCopy() { QRCode = request.QRCode });

            bookProduct.DomainEvents.Add(new BookAddedEvent(bookProduct));

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
