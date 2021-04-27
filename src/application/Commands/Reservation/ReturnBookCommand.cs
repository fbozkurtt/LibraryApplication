using LibraryApplication.Application.Common.Interfaces;
using LibraryApplication.Application.Common.Models;
using LibraryApplication.Constants;
using LibraryApplication.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Commands.Reservation
{
    [Authorize(Roles = DefaultRoleNames.User)]
    public class ReturnBookCommand : IRequest<ReturnBookResponse>
    {
        [Required]
        public string QRCode { get; set; }

        public decimal? Fee { get; set; }
    }

    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, ReturnBookResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public ReturnBookCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ReturnBookResponse> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var bookReservation = await _dbContext.BookReservations.SingleOrDefaultAsync(r =>
                r.BookCopy.QRCode.Equals(request.QRCode) && r.Returned.Equals(false));

            if (bookReservation == null)
                throw new Exception("Book is already returned or book reservation does not exist.");

            var daysPassedSinceReservationEndDate = (DateTime.Now - bookReservation.ReservationEnds).TotalDays;

            if (daysPassedSinceReservationEndDate > 0)
            {
                bookReservation.TotalFee = bookReservation.DailyExpirationFee * (decimal)daysPassedSinceReservationEndDate;

                if (request.Fee < bookReservation.TotalFee)
                {
                    await _dbContext.SaveChangesAsync();

                    throw new Exception($"{daysPassedSinceReservationEndDate} days have been past since return date." +
                        $" You have to pay {bookReservation.TotalFee} before returning the book.");
                }

                request.Fee -= bookReservation.TotalFee;
            }

            bookReservation.BookCopy.Reserved = false;
            bookReservation.Returned = true;

            bookReservation.DomainEvents.Add(new BookReturnedEvent(bookReservation));

            await _dbContext.SaveChangesAsync();

            return new ReturnBookResponse()
            {
                Success = true,
                Message = bookReservation.TotalFee.HasValue ? $"Paid {bookReservation.TotalFee.Value}$ for being late for returning the book." : string.Empty
            };
        }
    }
}
