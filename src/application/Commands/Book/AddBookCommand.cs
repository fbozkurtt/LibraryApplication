using LibraryApplication.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryApplication.Domain.Entities;
using LibraryApplication.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using LibraryApplication.Domain.Events;

namespace LibraryApplication.Application.Commands.Book
{
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
            var book = await _dbContext.Books.SingleOrDefaultAsync(b => b.ISBN.Equals(request.ISBN));

            if (book == null)
                throw new NotFoundException(
                    nameof(Domain.Entities.Book),
                    nameof(Domain.Entities.Book.ISBN),
                    request.ISBN);

            var bookProduct = new BookProduct() { BookId = book.Id.Value, QRCode = request.QRCode };

            _dbContext.BookProducts.Add(bookProduct);

            bookProduct.DomainEvents.Add(new BookAddedEvent(bookProduct));

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
