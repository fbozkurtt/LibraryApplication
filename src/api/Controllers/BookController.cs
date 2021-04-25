using LibraryApplication.Application.Common.Models;
using LibraryApplication.Application.DTOs;
using LibraryApplication.Application.Queries.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApplication.Web.API.Controllers
{
    [Authorize]
    public class BookController : ApiControllerBase
    {
        [HttpGet]
        public async Task<PaginatedList<BookDto>> Books([FromQuery] GetBooksQuery query)
            => await Mediator.Send(query);
    }
}
