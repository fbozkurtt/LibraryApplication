using LibraryApplication.Application.Common.Exceptions;
using LibraryApplication.Application.Common.Interfaces;
using LibraryApplication.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Commands.Reservation
{
    public class ReserveBookCommand : IRequest<string>
    {
        public long ISBN { get; set; }
    }

    public class ReserveBookCommandHandler : IRequestHandler<ReserveBookCommand, string>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;

        public ReserveBookCommandHandler(
            IApplicationDbContext dbContext,
            ICurrentUserService currentUserService,
            IIdentityService identityService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task<string> Handle(ReserveBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _dbContext.Books.SingleOrDefaultAsync(b => b.ISBN.Equals(request.ISBN));

            if (book == null)
                throw new NotFoundException(
                    nameof(Domain.Entities.Book),
                    nameof(Domain.Entities.Book.ISBN),
                    request.ISBN);

            var bookToReserve = book.BooksInInventory.FirstOrDefault(b => b.Reserved.Equals(false));

            if (bookToReserve == null)
                throw new Exception("There is no book available to reserve.");

            var user = await _identityService.GetUserAsync(_currentUserService.UserId);

            if (user == null)
                throw new Exception("User not found.");

            await _dbContext.BookReservations.AddAsync(new BookReservation() 
            {
                UserId = user.Id,
                BookProductId = bookToReserve.Id.Value,
                ReservationEnds = DateTime.Now + TimeSpan.FromDays(7),
                DailyExpirationFee = 0.50m
            });

            return bookToReserve.QRCode;
        }
    }
}
