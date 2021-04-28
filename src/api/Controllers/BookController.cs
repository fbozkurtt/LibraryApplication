using LibraryApplication.Application.Commands.Book;
using LibraryApplication.Application.Common.Models;
using LibraryApplication.Application.DTOs;
using LibraryApplication.Application.Queries.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryApplication.Web.API.Controllers
{
    [Authorize]
    public class BookController : ApiControllerBase
    {
        [HttpGet]
        public async Task<PaginatedList<BookDto>> GetBooks([FromQuery] GetBooksQuery query)
            => await Mediator.Send(query);

        [HttpPost]
        public async Task<ActionResult> CreateBookMeta(CreateBookCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> AddBookToInventory([FromQuery] AddBookCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBookMeta([FromQuery] UpdateBookCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBookMeta([FromQuery] DeleteBookCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
