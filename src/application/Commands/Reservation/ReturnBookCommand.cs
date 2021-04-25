using LibraryApplication.Application.Common.Interfaces;
using LibraryApplication.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Commands.Reservation
{
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
                r.Book.QRCode.Equals(request.QRCode));

            if (bookReservation.Returned)
            {
                return new ReturnBookResponse()
                {
                    Success = false,
                    Message = $"Book is already returned."
                };
            }

            var daysPassedSinceReversationEndDate = (DateTime.Now - bookReservation.ReservationEnds).TotalDays;

            if (daysPassedSinceReversationEndDate > 0)
            {
                bookReservation.TotalFee = bookReservation.DailyExpirationFee * (decimal)daysPassedSinceReversationEndDate;

                if (request.Fee >= bookReservation.TotalFee)
                {
                    request.Fee -= bookReservation.TotalFee;

                    bookReservation.Returned = true;

                    await _dbContext.SaveChangesAsync();

                    return new ReturnBookResponse()
                    {
                        Success = true,
                        Message = $"You have returned the book with a fee. You have left for change {request.Fee}$"
                    };

                }

                await _dbContext.SaveChangesAsync();

                return new ReturnBookResponse()
                {
                    Success = false,
                    Message = $"{daysPassedSinceReversationEndDate} days has been past since return date." +
                    $" You have to pay {bookReservation.TotalFee} before returning the book."
                };
            }

            bookReservation.Returned = true;

            await _dbContext.SaveChangesAsync();

            return new ReturnBookResponse()
            {
                Success = true
            };
        }
    }
}
