using LibraryApplication.Application.Commands.Reservation;
using LibraryApplication.Application.Common.Models;
using LibraryApplication.Application.DTOs;
using LibraryApplication.Application.Queries.Reservation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApplication.Web.API.Controllers
{
    public class ReservationController : ApiControllerBase
    {
        [HttpGet]
        public async Task<PaginatedList<BookReservationDto>> GetReservations([FromQuery] GetReservedBooksQuery query)
            => await Mediator.Send(query);

        [HttpPost]
        public async Task<string> ReserveBook([FromQuery] ReserveBookCommand command)
            => await Mediator.Send(command);

        [HttpPut]
        public async Task<ReturnBookResponse> ReturnBook(ReturnBookCommand command)
            => await Mediator.Send(command);
    }
}
