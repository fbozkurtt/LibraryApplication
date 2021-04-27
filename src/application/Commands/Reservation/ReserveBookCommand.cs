using LibraryApplication.Application.Common.Exceptions;
using LibraryApplication.Application.Common.Interfaces;
using LibraryApplication.Constants;
using LibraryApplication.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Commands.Reservation
{
    [Authorize(Roles = DefaultRoleNames.User)]
    public class ReserveBookCommand : IRequest<string>
    {
        public long ISBN { get; }
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
            var bookMeta = await _dbContext.BookMetas.SingleOrDefaultAsync(b => b.ISBN.Equals(request.ISBN));

            if (bookMeta == null)
                throw new NotFoundException(
                    nameof(BookMeta),
                    nameof(request.ISBN),
                    request.ISBN);

            var bookToReserve = bookMeta.BooksInInventory.FirstOrDefault(b => b.Reserved.Equals(false));

            if (bookToReserve == null)
                throw new Exception("There is no book available to borrow.");

            var user = await _identityService.GetUserAsync(_currentUserService.UserId);

            if (user == null)
                throw new Exception("User not found.");

            bookToReserve.Reserved = true;

            var bookReservation = new BookReservation()
            {
                UserId = user.Id,
                ReservationEnds = DateTime.Now + TimeSpan.FromDays(7),
                DailyExpirationFee = 0.50m,
                BookCopy = bookToReserve
            };


            await _dbContext.BookReservations.AddAsync(bookReservation);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return bookToReserve.QRCode;
        }
    }
}
